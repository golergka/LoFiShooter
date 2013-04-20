using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

// This is the basic class of monobehavior for ALL scripts in the project.
// This is the only class that is supposed to be a direct child of MonoBehavior
public abstract class BasicBehavior : MonoBehaviour {

	protected IEnumerable<FieldInfo> GetAllFieldsWithAttribute(Type attributeType) {

		return this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(
			f => f.GetCustomAttributes(attributeType, false).Any() );

	}

	protected static List<BasicBehavior> behaviors = new List<BasicBehavior>();

	protected virtual void Awake() {

		behaviors.Add(this);

		if (!singletons.ContainsKey(this.GetType())) {
			singletons.Add(this.GetType(), new SingletonHolder(this));
		} else {

			SingletonHolder holder = singletons[this.GetType()];
			holder.allowed = false;
			singletons[this.GetType()] = holder;

		}

		// All fields with [SeupableField] attribute should be set up by designer.
		// We're checking that.
		foreach(var s in GetAllFieldsWithAttribute(typeof(SetupableField))) {

			if (s.GetValue(this) == null) {

				Debug.LogWarning("Variable " + s.ToString() + " should be set up beforehand!");
				enabled = false;

			}

		}

		// All fields with [ComponentField] attribte are just links to components on this gameObject.
		// We are creating this links.
		foreach(var s in GetAllFieldsWithAttribute(typeof(ComponentField))) {

			var component = GetComponent(s.FieldType.Name);

			if (component == null) {

				if ( s.FieldType.GetFields().Length == 0 ) {

					/*
					In case this kind of component isn't configurable by designer (doesn't have any public fields),
					we can create it ourselves. No big deal.
					*/

					component = gameObject.AddComponent(s.FieldType);
					//Debug.Log("Added component " + component.ToString() );

					if (component != null) {

						s.SetValue(this, component);

					} else {

						Debug.LogWarning("Failed to create component " + s.FieldType.Name);
						enabled = false;

					}

				} else {

					// But if it has public fields, it should be added beforehand.

					Debug.LogWarning("No component of type " + s.FieldType.Name + " found on the object!");
					enabled = false;

				}

			} else {

				s.SetValue(this, component);

			}

		}

	}

	static Dictionary<System.Type,SingletonHolder> singletons = new Dictionary<System.Type,SingletonHolder>();

	struct SingletonHolder {
		public object instance;
		public bool allowed;

		public SingletonHolder(object instance) {
			this.instance = instance;
			allowed = true;
		}

	}

	public static T Singleton<T>() where T : UnityEngine.Component {

		if (singletons.ContainsKey(typeof(T))) {

			if (singletons[typeof(T)].allowed) {
				return (T) singletons[typeof(T)].instance;
			} else {
				Debug.LogError("Type " + typeof(T).Name + " has more than one occasion, can't access it as a singleton!");
				return default(T);
			}

		} else {

			GameObject singletonGameObject = new GameObject(typeof(T).Name + " singleton");
			T singleton = singletonGameObject.AddComponent<T>();
			return singleton;

		}		

	}

	protected virtual void Start() {

		OnGameReset();

	}

	public abstract void OnGameReset();

	new public UnityEngine.Object Instantiate(UnityEngine.Object objectToCreate, Vector3 position, Quaternion rotation) {

		UnityEngine.Object newObject = base.Instantiate (objectToCreate, position, rotation);

		if (newObject is GameObject) {

			( (GameObject) newObject ).AddComponent<DestroyOnReset>();

		}

		return newObject;

	}

}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class SetupableField : System.Attribute {
	
}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ComponentField : System.Attribute {

}