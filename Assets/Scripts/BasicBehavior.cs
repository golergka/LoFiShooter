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

	protected virtual void Awake() {

		foreach(var s in GetAllFieldsWithAttribute(typeof(SetupableField))) {

			if (s.GetValue(this) == null) {

				Debug.LogWarning("Variable " + s.ToString() + " should be set up beforehand!");
				enabled = false;

			}

		}

		foreach(var s in GetAllFieldsWithAttribute(typeof(ComponentField))) {

			var component = GetComponent(s.FieldType.Name);

			if (component == null) {

				Debug.LogWarning("No component of type " + s.FieldType.Name + " found on the object!");
				enabled = false;

			} else {

				s.SetValue(this, component);

			}

		}

	}

}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class SetupableField : System.Attribute {
	
}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ComponentField : System.Attribute {

}