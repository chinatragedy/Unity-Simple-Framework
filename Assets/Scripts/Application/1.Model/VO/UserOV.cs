using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class UserOV {
	public string Name { get; set; }
	public int Pwd { get; set; }

	public UserOV(){ }

	public UserOV(string name, int pwd)
	{
		Name = name;
		Pwd = pwd;
	}
}

[SerializeField]
public class UserInfoOV
{
	public string Id { get; set; }
	public int Age { get; set; }

	public UserInfoOV() { }

	public UserInfoOV(string id, int age)
	{
		Id = id;
		Age = age;
	}
}