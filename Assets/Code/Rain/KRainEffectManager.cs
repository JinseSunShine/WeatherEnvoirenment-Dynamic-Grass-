using UnityEngine;
using System.Collections;



public class KRainEffectManager : MonoBehaviour
{
	GameObject _raineffect = null;
	GameObject _rainsplash = null;

	void Start()
	{
		_raineffect = transform.FindChild("RainEffect").gameObject;
		_rainsplash = transform.FindChild("RainSplash").gameObject;
	}

	public void EnableRainEffect(bool bEnable)
	{
		_raineffect.SetActive(bEnable);
	}

	public void EnableRainSplash(bool bEnable)
	{
		_rainsplash.SetActive(bEnable);
	}
}

