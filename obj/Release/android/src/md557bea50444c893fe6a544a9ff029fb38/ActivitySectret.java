package md557bea50444c893fe6a544a9ff029fb38;


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
		mono.android.Runtime.register ("Gato_Tic_Tac_Toe.ActivitySectret, Gato Tic Tac Toe", ActivitySectret.class, __md_methods);
	}


	public ActivitySectret ()
	{
		super ();
		if (getClass () == ActivitySectret.class)
			mono.android.TypeManager.Activate ("Gato_Tic_Tac_Toe.ActivitySectret, Gato Tic Tac Toe", "", this, new java.lang.Object[] {  });
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
