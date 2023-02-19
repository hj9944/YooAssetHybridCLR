public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ constraint implement type
	// }} 

	// {{ AOT generic type
	//System.Action`1<System.Object>
	//System.Collections.Generic.IEnumerator`1<System.Object>
	//UniFramework.Module.ModuleSingleton`1<System.Object>
	// }}

	public void RefMethods()
	{
		// System.Object[] System.Array::Empty<System.Object>()
		// System.Void UniFramework.Event.EventGroup::AddListener<System.Object>(System.Action`1<UniFramework.Event.IEventMessage>)
		// System.Void UniFramework.Machine.StateMachine::AddNode<System.Object>()
		// System.Void UniFramework.Machine.StateMachine::ChangeState<System.Object>()
		// System.Void UniFramework.Machine.StateMachine::Run<System.Object>()
		// System.Object UniFramework.Module.UniModule::CreateModule<System.Object>(System.Int32)
		// System.Void UniFramework.Window.UniWindow::CloseWindow<System.Object>()
		// UniFramework.Window.OpenWindowOperation UniFramework.Window.UniWindow::OpenWindowAsync<System.Object>(System.String,System.Object[])
		// System.Object UnityEngine.Component::GetComponent<System.Object>()
		// System.Object UnityEngine.GameObject::AddComponent<System.Object>()
		// System.Object UnityEngine.GameObject::GetComponent<System.Object>()
		// YooAsset.AssetOperationHandle YooAsset.YooAssets::LoadAssetAsync<System.Object>(System.String)
	}
}