package md5ed267ff0e1980c9a4aea120edc6cd8ef;


public class ActivitySectret
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Gato_Tic_Tac_Toe.ActivitySectret, Gato Tic Tac Toe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivitySectret.class, __md_methods);
	}


	public ActivitySectret ()
	{
		super ();
		if (getClass () == ActivitySectret.class)
			mono.android.TypeManager.Activate ("Gato_Tic_Tac_Toe.ActivitySectret, Gato Tic Tac Toe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
