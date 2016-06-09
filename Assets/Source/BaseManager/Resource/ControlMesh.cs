using UnityEngine;
using System.Collections;


public class ControlMesh
{
	public SpriteRoot.SPRITE_PLANE	m_plane		= SpriteRoot.SPRITE_PLANE.XY;
	public SpriteRoot.WINDING_ORDER	m_winding	= SpriteRoot.WINDING_ORDER.CW;
	public SpriteRoot.ANCHOR_METHOD	m_anchor	= SpriteRoot.ANCHOR_METHOD.TEXTURE_OFFSET;
	
	public float	m_width			= 0.0f;
	public float	m_height		= 0.0f;
	public bool		pixelPerfect	= false;
	public bool		autoResize		= false;
	
	public Vector2	m_bleedCompensation;
	public Vector3	m_offset		= new Vector3();
	public Color	m_color			= Color.white;
	
	protected Vector2	bleedCompensationUV;
	protected Vector2	bleedCompensationUVMax;
	
	protected SPRITE_FRAME frameInfo		= new SPRITE_FRAME(0);
	protected Vector2	scaleFactor			= new Vector2(0.5f, 0.5f);
	protected Vector2	topLeftOffset		= new Vector2(-1f, 1f);
	protected Vector2	bottomRightOffset	= new Vector2(1f, -1f);
	protected Vector3	topLeft;
	protected Vector3	bottomRight;
	
	protected Rect		uvRect;

	protected Vector3	unclippedTopLeft;
	protected Vector3	unclippedBottomRight;
	
	protected Vector2	tlTruncate = new Vector2(1f, 1f);
	protected Vector2	brTruncate = new Vector2(1f, 1f);
	protected bool		truncated;
	protected Rect3D	clippingRect;
	protected Rect		localClipRect;
	protected float		leftClipPct		= 1f;
	protected float		rightClipPct	= 1f;
	protected float		topClipPct		= 1f;
	protected float		bottomClipPct	= 1f;
	protected bool		clipped			= false;
	
	public		bool	uvsInitialized	= false;
	protected	bool	m_started		= false;
	protected	bool	deleted			= false;
	
	protected	GameObject	m_ControlObject;
	
	protected ISpriteMesh m_spriteMesh;
	protected ISpriteAnimatable m_prev;
	protected ISpriteAnimatable m_next;
	protected Vector2 screenSize;
	protected Vector2 sizeUnitsPerUV;
	public Vector2 pixelsPerUV;
	protected float worldUnitsPerScreenPixel;
	protected EZScreenPlacement screenPlacer;
	protected bool m_hidden = false;
	public bool ignoreClipping = false;
	protected SpriteRootMirror mirror = null;
	
	protected Vector2 tempUV;
	protected Mesh oldMesh;
	protected SpriteManager savedManager;
	
	public void Create(GameObject _obj)
	{
		m_ControlObject	= _obj;
		
		screenSize.x = 0;
		screenSize.y = 0;
		
		if(m_ControlObject != null)
		{
			MeshFilter mf = (MeshFilter)m_ControlObject.GetComponent(typeof(MeshFilter));
			if (mf != null)
			{
				oldMesh = mf.sharedMesh;
				mf.sharedMesh = null;
			}
		}
		AddMesh();
		Init();
	}
	
	public GameObject gameObject
	{
		get { return m_ControlObject; }
	}
	
	public Transform transform
	{
		get { return m_ControlObject.transform; }
	}
	
	public Renderer renderer
	{
		get { return m_ControlObject.GetComponent<Renderer>(); }
	}
	
	protected void CalcSizeUnitsPerUV()
	{
		Rect uvs = frameInfo.uvs;
		
		if (uvs.width == 0 || uvs.height == 0 || (uvs.xMin == 1f && uvs.yMin == 1f))
		{
			sizeUnitsPerUV = Vector2.zero;
			return;
		}
		
		sizeUnitsPerUV.x = m_width / uvs.width;
		sizeUnitsPerUV.y = m_height / uvs.height;
	}
	
	protected void Init()
	{
		screenPlacer = (EZScreenPlacement)m_ControlObject.GetComponent(typeof(EZScreenPlacement));
		
		if(m_spriteMesh != null)
		{
			if (m_spriteMesh.texture != null)
			{
				SetPixelToUV(m_spriteMesh.texture);
			}
			m_spriteMesh.Init();
		}
		
		if (!Application.isPlaying)
			CalcSizeUnitsPerUV();
	}
	
	public void SetValid(bool IsValid)
	{
		if(m_ControlObject != null)
		{
			m_ControlObject.SetActiveRecursively(IsValid);
		}
	}
	
	public void Clear()
	{
		color = Color.white;
		m_offset = Vector3.zero;
	}
	
	public void InitUVs()
	{
		uvRect = frameInfo.uvs;
	}
	
	public void Delete()
	{
		deleted = true;
		
		if(Application.isPlaying)
		{
			GameObject.Destroy(((SpriteMesh)spriteMesh).mesh);
			((SpriteMesh)spriteMesh).mesh = null;
		}
	}

	public void OnDestroy()
	{
		Delete();
	}
	
	
#region get / set
	public SpriteRoot.SPRITE_PLANE SpritePlane	{
		get { return m_plane; }
		set {
			m_plane = value;
			SetSize();
		}
	}
	
	public SpriteRoot.WINDING_ORDER Winding	{
		get { return m_winding; }
		set {
			m_winding = value;
			if(m_spriteMesh != null)
				((SpriteMesh)m_spriteMesh).SetWindingOrder(value);
		}
	}
	
	public SpriteRoot.ANCHOR_METHOD Anchor	{
		get { return m_anchor; }
		set {
			m_anchor	= value;
			SetSize();
		}
	}
	
	public Vector2 bleedCompensation	{
		get { return m_bleedCompensation; }
		set {
			SetBleedCompensation(value);
		}
	}
	
	public Vector3 offset	{
		get { return m_offset; }
		set {
			m_offset = value;
			SetSize();
		}
	}
	
	public Color color	{
		get { return m_color; }
		set {
			m_color = value;
			
			if(m_spriteMesh != null)
				m_spriteMesh.UpdateColors(m_color);
		}
	}
	
	public bool Clipped	{
		get { return clipped; }
		set
		{
			if (ignoreClipping)
				return;
			
			if (value && !clipped)
			{
				clipped = true;
				CalcSize();
			}
			else if (clipped)
				Unclip();
		}
	}
	
	public bool Started	{
		get { return m_started; }
	}
	
	public Vector2 PixelSize	{
		get { return new Vector2(m_width * worldUnitsPerScreenPixel, m_height * worldUnitsPerScreenPixel); }
		set
		{
			SetSize(value.x * worldUnitsPerScreenPixel, value.y * worldUnitsPerScreenPixel);
		}
	}
	
	public Vector2 ImageSize	{
		get { return new Vector2(uvRect.width * pixelsPerUV.x, uvRect.height * pixelsPerUV.y);  }
	}
	
	public Rect3D ClippingRect	{
		get { return clippingRect; }
		set
		{
			if (ignoreClipping)
				return;
			
			clippingRect = value;
			localClipRect = Rect3D.MultFast(clippingRect, transform.worldToLocalMatrix).GetRect();
			clipped = true;
			CalcSize();
			UpdateUVs();
		}
	}
	
	public Vector3 UnclippedTopLeft	{
		get
		{
			if (!m_started)
				Start();
			
			return unclippedTopLeft; 
		}
	}
	
	public Vector3 UnclippedBottomRight	{
		get
		{
			if (!m_started)
				Start();
			return unclippedBottomRight; 
		}
	}
	
	public Vector3 TopLeft	{
		get
		{
			if (m_spriteMesh != null)
				return m_spriteMesh.vertices[0];
			else
				return Vector3.zero;
		}
	}
	
	public Vector3 BottomRight	{
		get
		{
			if (m_spriteMesh != null)
				return m_spriteMesh.vertices[2];
			else
				return Vector3.zero;
		}
	}
	
	public ISpriteMesh spriteMesh	{
		get { return m_spriteMesh; }
		set
		{
			m_spriteMesh = value;
			if (m_spriteMesh != null)
			{
				if (m_spriteMesh.control != this)
					m_spriteMesh.control = this;
			}
			else
				return;
		}
	}
	
	public ISpriteAnimatable prev	{
		get { return m_prev; }
		set { m_prev = value; }
	}

	public ISpriteAnimatable next	{
		get { return m_next; }
		set { m_next = value; }
	}
#endregion
	
#region Set
	public void SetBleedCompensation()					{ SetBleedCompensation(m_bleedCompensation); }
	public void SetBleedCompensation(float x, float y)	{ SetBleedCompensation(new Vector2(x, y)); }
	
	protected void SetBleedCompensation(Vector2 xy)
	{
		m_bleedCompensation = xy;
		bleedCompensationUV = PixelSpaceToUVSpace(m_bleedCompensation);
		bleedCompensationUVMax = bleedCompensationUV * -2f;
		
		uvRect.x += bleedCompensationUV.x;
		uvRect.y += bleedCompensationUV.y;
		uvRect.xMax += bleedCompensationUVMax.x;
		uvRect.yMax += bleedCompensationUVMax.y;
		
		UpdateUVs();
	}
	
	public void SetFrameInfo(SPRITE_FRAME fInfo)
	{
		frameInfo = fInfo;
		uvRect = fInfo.uvs;
		
		SetBleedCompensation();
		
		if (autoResize || pixelPerfect)
			CalcSize();
	}
	
	public void SetPixelToUV(int texWidth, int texHeight)
	{
		Vector2 oldPPUV = pixelsPerUV;
		
		pixelsPerUV.x = texWidth;
		pixelsPerUV.y = texHeight;
		
		Rect uvs = frameInfo.uvs;
		
		if (uvs.width == 0 || uvs.height == 0 || oldPPUV.x == 0 || oldPPUV.y == 0)
			return;
		Vector2 sizePerTexel = new Vector2(m_width / (uvs.width * oldPPUV.x), m_height / (uvs.height * oldPPUV.y));
		sizeUnitsPerUV.x = sizePerTexel.x * pixelsPerUV.x;
		sizeUnitsPerUV.y = sizePerTexel.y * pixelsPerUV.y;
	}
	
	public void SetPixelToUV(Texture tex)
	{
		if (tex == null)
			return;
		SetPixelToUV(tex.width, tex.height);
	}
	
	public void SetUVs(Rect uv)
	{
		frameInfo.uvs = uv;
		uvRect = uv;
		
		SetBleedCompensation();
		
		if (!Application.isPlaying)
			CalcSizeUnitsPerUV();
			
		if (autoResize || pixelPerfect)
			CalcSize();
	}
	
	public void SetUVsFromPixelCoords(Rect pxCoords)
	{
		tempUV = PixelCoordToUVCoord((int)pxCoords.x, (int)pxCoords.yMax);
		uvRect.x = tempUV.x;
		uvRect.y = tempUV.y;
		
		tempUV = PixelCoordToUVCoord((int)pxCoords.xMax, (int)pxCoords.y);
		uvRect.xMax = tempUV.x;
		uvRect.yMax = tempUV.y;
		
		frameInfo.uvs = uvRect;
		
		SetBleedCompensation();
		
		if (autoResize || pixelPerfect)
			CalcSize();
	}
#endregion
	
#region Get
	public Vector3[] GetVertices()
	{
		return ((SpriteMesh)m_spriteMesh).mesh.vertices;
	}
	
	public Rect GetUVs()
	{
		return uvRect;
	}
	
	public Vector3 GetCenterPoint()
	{
		if (m_spriteMesh == null)
			return Vector3.zero;
		
		Vector3[] verts = m_spriteMesh.vertices;
		
		switch(m_plane)
		{
			case SpriteRoot.SPRITE_PLANE.XY:
				return new Vector3(verts[0].x + 0.5f * (verts[2].x - verts[0].x), verts[0].y - 0.5f * (verts[0].y - verts[2].y), m_offset.z);
			case SpriteRoot.SPRITE_PLANE.XZ:
				return new Vector3(verts[0].x + 0.5f * (verts[2].x - verts[0].x), m_offset.y, verts[0].z - 0.5f * (verts[0].z - verts[2].z));
			case SpriteRoot.SPRITE_PLANE.YZ:
				return new Vector3(m_offset.x, verts[0].y - 0.5f * (verts[0].y - verts[2].y), verts[0].z - 0.5f * (verts[0].z - verts[2].z));
			default:
				return new Vector3(verts[0].x + 0.5f * (verts[2].x - verts[0].x), verts[0].y - 0.5f * (verts[0].y - verts[2].y), m_offset.z);
		}
	}
	
	public Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		Vector2 size = Vector2.zero;
		return size;
	}
#endregion
	
	public void CalcEdges()
	{
		switch (m_anchor)
		{
			case SpriteRoot.ANCHOR_METHOD.TEXTURE_OFFSET:
			Vector2 halfSizeIfFull;
			halfSizeIfFull.x = m_width * scaleFactor.x;
			halfSizeIfFull.y = m_height * scaleFactor.y;
			
			topLeft.x = halfSizeIfFull.x * topLeftOffset.x;
			topLeft.y = halfSizeIfFull.y * topLeftOffset.y;
			bottomRight.x = halfSizeIfFull.x * bottomRightOffset.x;
			bottomRight.y = halfSizeIfFull.y * bottomRightOffset.y;
			break;
		case SpriteRoot.ANCHOR_METHOD.UPPER_LEFT:
			topLeft.x = 0;
			topLeft.y = 0;
			bottomRight.x = m_width;
			bottomRight.y = -m_height;
			break;
		case SpriteRoot.ANCHOR_METHOD.UPPER_CENTER:
			topLeft.x = m_width * -0.5f;
			topLeft.y = 0;
			bottomRight.x = m_width * 0.5f;
			bottomRight.y = -m_height;
			break;
		case SpriteRoot.ANCHOR_METHOD.UPPER_RIGHT:
			topLeft.x = -m_width;
			topLeft.y = 0;
			bottomRight.x = 0;
			bottomRight.y = -m_height;
			break;
		case SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT:
			topLeft.x = 0;
			topLeft.y = m_height * 0.5f;
			bottomRight.x = m_width;
			bottomRight.y = m_height * -0.5f;
			break;
		case SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER:
			topLeft.x = m_width * -0.5f;
			topLeft.y = m_height * 0.5f;
			bottomRight.x = m_width * 0.5f;
			bottomRight.y = m_height * -0.5f;
			break;
		case SpriteRoot.ANCHOR_METHOD.MIDDLE_RIGHT:
			topLeft.x = -m_width;
			topLeft.y = m_height * 0.5f;
			bottomRight.x = 0;
			bottomRight.y = m_height * -0.5f;
			break;
		case SpriteRoot.ANCHOR_METHOD.BOTTOM_LEFT:
			topLeft.x = 0;
			topLeft.y = m_height;
			bottomRight.x = m_width;
			bottomRight.y = 0;
			break;
		case SpriteRoot.ANCHOR_METHOD.BOTTOM_CENTER:
			topLeft.x = m_width * -0.5f;
			topLeft.y = m_height;
			bottomRight.x = m_width * 0.5f;
			bottomRight.y = 0;
			break;
		case SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT:
			topLeft.x = -m_width;
			topLeft.y = m_height;
			bottomRight.x = 0;
			bottomRight.y = 0;
			break;
		}

		unclippedTopLeft = topLeft + m_offset;
		unclippedBottomRight = bottomRight + m_offset;

		if (truncated)
		{
			topLeft.x = bottomRight.x - (bottomRight.x - topLeft.x) * tlTruncate.x;
			topLeft.y = bottomRight.y - (bottomRight.y - topLeft.y) * tlTruncate.y;
			bottomRight.x = topLeft.x - (topLeft.x - bottomRight.x) * brTruncate.x;
			bottomRight.y = topLeft.y - (topLeft.y - bottomRight.y) * brTruncate.y;
		}
		
		if(clipped && bottomRight.x - topLeft.x != 0 && topLeft.y - bottomRight.y != 0)
		{
			Vector3 origTL = topLeft;
			Vector3 origBR = bottomRight;
			Rect tempClipRect = localClipRect;
			
			tempClipRect.x -= m_offset.x;
			tempClipRect.y -= m_offset.y;
			
			if (topLeft.x < tempClipRect.x)
			{
				leftClipPct = 1f - (tempClipRect.x - origTL.x) / (origBR.x - origTL.x);
				topLeft.x = Mathf.Clamp(tempClipRect.x, origTL.x, origBR.x);
				
				if (leftClipPct <= 0)
					topLeft.x = bottomRight.x = tempClipRect.x;
			}
			else
				leftClipPct = 1;
			
			if (bottomRight.x > tempClipRect.xMax)
			{
				rightClipPct = (tempClipRect.xMax - origTL.x) / (origBR.x - origTL.x);
				bottomRight.x = Mathf.Clamp(tempClipRect.xMax, origTL.x, origBR.x);
				
				if (rightClipPct <= 0)
					bottomRight.x = topLeft.x = tempClipRect.xMax;
			}
			else
				rightClipPct = 1;
			
			if (topLeft.y > tempClipRect.yMax)
			{
				topClipPct = (tempClipRect.yMax - origBR.y) / (origTL.y - origBR.y);
				topLeft.y = Mathf.Clamp(tempClipRect.yMax, origBR.y, origTL.y);

				if (topClipPct <= 0)
					topLeft.y = bottomRight.y = tempClipRect.yMax;
			}
			else
				topClipPct = 1;
			
			if (bottomRight.y < tempClipRect.y)
			{
				bottomClipPct = 1f - (tempClipRect.y - origBR.y) / (origTL.y - origBR.y);
				bottomRight.y = Mathf.Clamp(tempClipRect.y, origBR.y, origTL.y);
				
				if (bottomClipPct <= 0)
					bottomRight.y = topLeft.y = tempClipRect.y;
			}
			else
				bottomClipPct = 1;
		}
		
		if(m_winding == SpriteRoot.WINDING_ORDER.CCW)
		{
			topLeft.x *= -1f;
			bottomRight.x *= -1f;
		}
	}
	
	public void CalcSize()
	{
		if (uvRect.width == 0)
			uvRect.width = 0.0000001f;
		if (uvRect.height == 0)
			uvRect.height = 0.0000001f;
		
		if (pixelPerfect)
		{
			m_width = worldUnitsPerScreenPixel * frameInfo.uvs.width * pixelsPerUV.x;
			m_height = worldUnitsPerScreenPixel * frameInfo.uvs.height * pixelsPerUV.y;
		}
		else if (autoResize) // Else calculate the size based on the change in UV dimensions:
		{
			if (sizeUnitsPerUV.x != 0 && sizeUnitsPerUV.y != 0)
			{
				m_width = frameInfo.uvs.width * sizeUnitsPerUV.x;
				m_height = frameInfo.uvs.height * sizeUnitsPerUV.y;
			}
		}
		
		SetSize();
	}
	
	public void SetSize(float w, float h)
	{
		m_width		= w;
		m_height	= h;
		
		SetSize();
	}
	
	public void SetSize()
	{
		if (m_spriteMesh == null)
			return;
			
		CalcSizeUnitsPerUV();
		switch (m_plane)
		{
			case SpriteRoot.SPRITE_PLANE.XY:
				SetSizeXY(m_width, m_height);
				break;
			case SpriteRoot.SPRITE_PLANE.XZ:
				SetSizeXZ(m_width, m_height);
				break;
			case SpriteRoot.SPRITE_PLANE.YZ:
				SetSizeYZ(m_width, m_height);
				break;
		}
	}
	
	protected void SetSizeXY(float w, float h)
	{
		CalcEdges();

		Vector3[] vertices = m_spriteMesh.vertices;

		if(m_winding == SpriteRoot.WINDING_ORDER.CW)
		{
			// Upper-left
			vertices[0].x = m_offset.x + topLeft.x;
			vertices[0].y = m_offset.y + topLeft.y;
			vertices[0].z = m_offset.z;

			// Lower-left
			vertices[1].x = m_offset.x + topLeft.x;
			vertices[1].y = m_offset.y + bottomRight.y;
			vertices[1].z = m_offset.z;

			// Lower-right
			vertices[2].x = m_offset.x + bottomRight.x;
			vertices[2].y = m_offset.y + bottomRight.y;
			vertices[2].z = m_offset.z;

			// Upper-right
			vertices[3].x = m_offset.x + bottomRight.x;
			vertices[3].y = m_offset.y + topLeft.y;
			vertices[3].z = m_offset.z;
		}
		else
		{
			// Upper-left
			vertices[0].x = m_offset.x + topLeft.x;
			vertices[0].y = m_offset.y + topLeft.y;
			vertices[0].z = m_offset.z;

			// Lower-left
			vertices[1].x = m_offset.x + topLeft.x;
			vertices[1].y = m_offset.y + bottomRight.y;
			vertices[1].z = m_offset.z;

			// Lower-right
			vertices[2].x = m_offset.x + bottomRight.x;
			vertices[2].y = m_offset.y + bottomRight.y;
			vertices[2].z = m_offset.z;

			// Upper-right
			vertices[3].x = m_offset.x + bottomRight.x;
			vertices[3].y = m_offset.y + topLeft.y;
			vertices[3].z = m_offset.z;
		}
		m_spriteMesh.UpdateVerts();
	}
	
	protected void SetSizeXZ(float w, float h)
	{
		CalcEdges();

		Vector3[] vertices = m_spriteMesh.vertices;

		// Upper-left
		vertices[0].x = m_offset.x + topLeft.x;
		vertices[0].y = m_offset.y;
		vertices[0].z = m_offset.z + topLeft.y;

		// Lower-left
		vertices[1].x = m_offset.x + topLeft.x;
		vertices[1].y = m_offset.y;
		vertices[1].z = m_offset.z + bottomRight.y;

		// Lower-right
		vertices[2].x = m_offset.x + bottomRight.x;
		vertices[2].y = m_offset.y;
		vertices[2].z = m_offset.z + bottomRight.y;

		// Upper-right
		vertices[3].x = m_offset.x + bottomRight.x;
		vertices[3].y = m_offset.y;
		vertices[3].z = m_offset.z + topLeft.y;

		m_spriteMesh.UpdateVerts();
	}
	
	protected void SetSizeYZ(float w, float h)
	{
		CalcEdges();

		Vector3[] vertices = m_spriteMesh.vertices;

		// Upper-left
		vertices[0].x = m_offset.x;
		vertices[0].y = m_offset.y + topLeft.y;
		vertices[0].z = m_offset.z + topLeft.x;

		// Lower-left
		vertices[1].x = m_offset.x;
		vertices[1].y = m_offset.y + bottomRight.y;
		vertices[1].z = m_offset.z + topLeft.x;

		// Lower-right
		vertices[2].x = m_offset.x;
		vertices[2].y = m_offset.y + bottomRight.y;
		vertices[2].z = m_offset.z + bottomRight.x;

		// Upper-right
		vertices[3].x = m_offset.x;
		vertices[3].y = m_offset.y + topLeft.y;
		vertices[3].z = m_offset.z + bottomRight.x;

		m_spriteMesh.UpdateVerts();
	}
	
	public void TruncateRight(float pct)
	{
		tlTruncate.x = 1f;
		brTruncate.x = Mathf.Clamp01(pct);
		if (brTruncate.x < 1f || tlTruncate.y < 1f || brTruncate.y < 1f)
			truncated = true;
		else
		{
			Untruncate();
			return;
		}

		UpdateUVs();
		CalcSize();
	}
	
	public void TruncateLeft(float pct)
	{
		tlTruncate.x = Mathf.Clamp01(pct);
		brTruncate.x = 1f;
		if (tlTruncate.x < 1f || tlTruncate.y < 1f || brTruncate.y < 1f)
			truncated = true;
		else
		{
			Untruncate();
			return;
		}

		UpdateUVs();
		CalcSize();
	}
	
	public void TruncateTop(float pct)
	{
		tlTruncate.y = Mathf.Clamp01(pct);
		brTruncate.y = 1f;
		if (tlTruncate.y < 1f || tlTruncate.x < 1f || brTruncate.x < 1f)
			truncated = true;
		else
		{
			Untruncate();
			return;
		}

		UpdateUVs();
		CalcSize();
	}
	
	public void TruncateBottom(float pct)
	{
		tlTruncate.y = 1f;
		brTruncate.y = Mathf.Clamp01(pct);
		if (brTruncate.y < 1f || tlTruncate.x < 1f || brTruncate.x < 1f)
			truncated = true;
		else
		{
			Untruncate();
			return;
		}
		
		UpdateUVs();
		CalcSize();
	}
	
	public void Untruncate()
	{
		tlTruncate.x = 1f;
		tlTruncate.y = 1f;
		brTruncate.x = 1f;
		brTruncate.y = 1f;
		truncated = false;
		
		uvRect = frameInfo.uvs;
		SetBleedCompensation();
		CalcSize();
	}
	
	public void Unclip()
	{
		if (ignoreClipping)
			return;
		
		leftClipPct = 1f;
		rightClipPct = 1f;
		topClipPct = 1f;
		bottomClipPct = 1f;
		clipped = false;
		uvRect = frameInfo.uvs;
		SetBleedCompensation();
		CalcSize();
	}
	
	public void UpdateUVs()
	{
		scaleFactor			= frameInfo.scaleFactor;
		topLeftOffset		= frameInfo.topLeftOffset;
		bottomRightOffset	= frameInfo.bottomRightOffset;
		
		if (truncated)
		{
			uvRect.x		= frameInfo.uvs.xMax + bleedCompensationUV.x - (frameInfo.uvs.width) * tlTruncate.x * leftClipPct;
			uvRect.y		= frameInfo.uvs.yMax + bleedCompensationUV.y - (frameInfo.uvs.height) * brTruncate.y * bottomClipPct;
			uvRect.xMax		= frameInfo.uvs.x + bleedCompensationUVMax.x + (frameInfo.uvs.width) * brTruncate.x * rightClipPct;
			uvRect.yMax		= frameInfo.uvs.y + bleedCompensationUVMax.y + (frameInfo.uvs.height) * tlTruncate.y * topClipPct;
		}
		else if(clipped)
		{
			Rect baseUV		= Rect.MinMaxRect(frameInfo.uvs.x + bleedCompensationUV.x, frameInfo.uvs.y + bleedCompensationUV.y, frameInfo.uvs.xMax + bleedCompensationUVMax.x, frameInfo.uvs.yMax + bleedCompensationUVMax.y);
			uvRect.x		= Mathf.Lerp(baseUV.xMax, baseUV.x, leftClipPct);
			uvRect.y		= Mathf.Lerp(baseUV.yMax, baseUV.y, bottomClipPct);
			uvRect.xMax		= Mathf.Lerp(baseUV.x, baseUV.xMax, rightClipPct);
			uvRect.yMax		= Mathf.Lerp(baseUV.y, baseUV.yMax, topClipPct);
		}
		
		if (m_spriteMesh == null)
			return;
		
		Vector2[] uvs	= m_spriteMesh.uvs;
		uvs[0].x		= uvRect.x; uvs[0].y = uvRect.yMax;
		uvs[1].x		= uvRect.x; uvs[1].y = uvRect.y;
		uvs[2].x		= uvRect.xMax; uvs[2].y = uvRect.y;
		uvs[3].x		= uvRect.xMax; uvs[3].y = uvRect.yMax;
		
		m_spriteMesh.UpdateUVs();
	}
	
	public void CalcPixelToUV()
	{
		if (renderer != null && renderer.sharedMaterial != null && renderer.sharedMaterial.mainTexture != null)
			SetPixelToUV(renderer.sharedMaterial.mainTexture);
	}
	
	protected void DestroyMesh()
	{
		if(m_spriteMesh != null)
			m_spriteMesh.sprite = null;
		m_spriteMesh = null;
		// Destroy our unneeded components:
		if(renderer != null)
			GameObject.DestroyImmediate(renderer);
		Object filter = gameObject.GetComponent(typeof(MeshFilter));
		if(filter != null)
			GameObject.DestroyImmediate(filter);
	}
	
	protected void AddMesh()
	{
		m_spriteMesh = new SpriteMesh();
		m_spriteMesh.control = this;
	}
	
	public Vector2 PixelSpaceToUVSpace(Vector2 xy)
	{
		if (pixelsPerUV.x == 0 || pixelsPerUV.y == 0)
			return Vector2.zero;
		
		return new Vector2(xy.x / pixelsPerUV.x, xy.y / pixelsPerUV.y);
	}
	
	public Vector2 PixelSpaceToUVSpace(int x, int y)
	{
		return PixelSpaceToUVSpace(new Vector2((float)x, (float)y));
	}
	
	public Vector2 PixelCoordToUVCoord(Vector2 xy)
	{
		if (pixelsPerUV.x == 0 || pixelsPerUV.y == 0)
			return Vector2.zero;

		return new Vector2(xy.x / (pixelsPerUV.x - 1f), 1.0f - (xy.y / (pixelsPerUV.y - 1f)));
	}
	
	public Vector2 PixelCoordToUVCoord(int x, int y)
	{
		return PixelCoordToUVCoord(new Vector2((float)x, (float)y));
	}
	
	public void Hide(bool tf)
	{
		if (!m_started)
			Start();
		
		if (m_spriteMesh != null)
			m_spriteMesh.Hide(tf);
		m_hidden = tf;
	}
	
	public void Start()
	{
	}
}