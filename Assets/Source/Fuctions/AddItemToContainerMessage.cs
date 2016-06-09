using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class AddItemToContainerMessage
{
	public static void SetImageRaster(byte[] _byteFriendImage, GameObject _objImage)
	{
		Texture2D wwwFriendImage = new Texture2D((int)ScrollData.sizeMessageImage.x, (int)ScrollData.sizeMessageImage.y);
		MeshRenderer meshRenderer = (MeshRenderer)_objImage.GetComponent(typeof(MeshRenderer));
		
		if(_byteFriendImage != null)
		{
			wwwFriendImage.LoadImage(_byteFriendImage);
			meshRenderer.material.mainTexture = wwwFriendImage;
			
			CSpriteFrame[] frames = new CSpriteFrame[4]; // 4 arrays of frames, with 1 frame each    
			_objImage.GetComponent<UIButton>().SetUVs(new Rect(0,0,1,1));
			
			for(int i=0; i<frames.Length; ++i)
			{
				frames[i]		= new CSpriteFrame();
				frames[i].uvs	= new Rect(0,0,1,1);
				SPRITE_FRAME [] anim = new SPRITE_FRAME[1];
				anim[0]			= frames[i].ToStruct();
				_objImage.GetComponent<UIButton>().animations[i].SetAnim(anim);
			}
			
			_objImage.GetComponent<UIButton>().UpdateUVs();
		}
	}
}