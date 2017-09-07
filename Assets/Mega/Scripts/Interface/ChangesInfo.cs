using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ChangesInfo : MonoBehaviour
{
	public List<Term> termsList = new List<Term>();
	public Text termName;
	public GameObject[] colomns;
	public GameObject colomn;
	public GameObject content;
	public ScrollRect scrRect;
	public List<Shop> db = new List<Shop>();
	public ShopTable shopTable;
	public Table list;
	public Table shop;
	
	private bool hidden = true;
	private string temporaryString = "В данном периоде реконструкции на Вашем объекте будут произведены технические работы. К первому числу указанного квартала Вам необходимо освободить объект";
	public class Term
	{
		public string termName;

		public List<string> shops;
	}

	public class Shop
	{
		public string name;
		public string description;
		public string iconName;

		public Shop(string nm, string descr, string icn)
		{
			name = nm;
			description = descr;
			iconName = icn;
		}
	}

	public void SetShop(string name)
	{
		Shop newShop = db.Find(x => x.name == name);
		Debug.Log(name+" "+newShop+" "+newShop.name);
		shopTable.shopName.text = newShop.name;
		if (newShop.description =="")
		shopTable.shopDescription.text = temporaryString;
		//shopTable.icon
		list.RollOut();
		shop.RollIn();
	}

	public void FillDatabase()
	{
		db.Add(new Shop("STRADIVARIUS", "",""));
		db.Add(new Shop("BANANA REPUBLIC", "",""));
		db.Add(new Shop("LACOSTE", "",""));
		db.Add(new Shop("IMAGINARIUM", "",""));
		db.Add(new Shop("X.O", "",""));
		db.Add(new Shop("McNEAL", "",""));
		db.Add(new Shop("CHRISTIAN BERG", "",""));
		db.Add(new Shop("MARKS & SPENCER", "",""));
		db.Add(new Shop("EUROSET", "",""));
		db.Add(new Shop("SONY", "",""));
		db.Add(new Shop("BORK", "",""));
		db.Add(new Shop("ACCESSORIES", "",""));
		db.Add(new Shop("CK JEANS", "",""));
		db.Add(new Shop("PANDORA", "",""));
		db.Add(new Shop("IGLA", "",""));
		db.Add(new Shop("GIPFEL", "",""));
		db.Add(new Shop("Дороги <color=#787A79ff>Реконструкция</color>", "",""));
		db.Add(new Shop("CUP <color=#787A79ff>Плиточные работы</color>", "",""));
		db.Add(new Shop("Ex-DECATHLON AREA <color=#787A79ff>Ремонт</color>", "",""));
		db.Add(new Shop("MANGO", "",""));
		db.Add(new Shop("CORSO COMO", "",""));
		db.Add(new Shop("MASCOTTE", "",""));
		db.Add(new Shop("SWAROVSKI", "",""));
		db.Add(new Shop("MAC", "",""));
		db.Add(new Shop("REEBOK", "",""));
		db.Add(new Shop("REEBOK CLASSIC", "",""));
		db.Add(new Shop("LUSIO", "",""));
		db.Add(new Shop("MAC", "",""));
		db.Add(new Shop("STOCKMAN <color=#787A79ff>Перепланировка</color>", "",""));
		db.Add(new Shop("KIKO", "",""));
		db.Add(new Shop("YVES ROCHER", "",""));
		db.Add(new Shop("ILE DE BEAUTE", "",""));
		db.Add(new Shop("ILE DE BEAUTE", "",""));
		db.Add(new Shop("5 KARMANOV", "",""));
		db.Add(new Shop("KIEHL’S", "",""));
		db.Add(new Shop("BOBBI BROWN", "",""));
		db.Add(new Shop("DE FACTO", "",""));
		db.Add(new Shop("MARMALATO", "",""));
		db.Add(new Shop("BUSTIER", "",""));
		db.Add(new Shop("LUSH", "",""));
		db.Add(new Shop("KIRA PLASTININA", "",""));
		db.Add(new Shop("MARKS & SPENCER", "",""));
		db.Add(new Shop("ALBLONE", "",""));
		db.Add(new Shop("MEZZATORRE", "",""));
		db.Add(new Shop("ECONICA", "",""));
		db.Add(new Shop("STRELLSON", "",""));
		db.Add(new Shop("ECCO ECCO KIDS", "",""));
		db.Add(new Shop("STOCKMAN <color=#787A79ff>Реконструкция</color>", "",""));
		db.Add(new Shop("<color=#787A79ff>Новый служебный коридор</color>", "",""));
		db.Add(new Shop("<color=#787A79ff>Перемещение коммунальных предприятий</color>", "",""));
		db.Add(new Shop("IKEA <color=#787A79ff>Реконструкция</color>", "",""));
		db.Add(new Shop("Парковка <color=#787A79ff>1 уровень</color>", "",""));
		db.Add(new Shop("LAUREN VIDAL", "",""));
		db.Add(new Shop("U.S. POLO", "",""));
		db.Add(new Shop("GUESS", "",""));
		db.Add(new Shop("QUIKSILVER", "",""));
		db.Add(new Shop("LEGO", "",""));
		db.Add(new Shop("BERSHKA", "",""));
		db.Add(new Shop("SUPERSTEP", "",""));
		db.Add(new Shop("OYSHO", "",""));
		db.Add(new Shop("BENETTON", "",""));
		db.Add(new Shop("SCOTCH & SODA", "",""));
		db.Add(new Shop("VICTORIA SECRET", "",""));
		db.Add(new Shop("SAMSONITE", "",""));
		db.Add(new Shop("JEANS SYMPHONY", "",""));
		db.Add(new Shop("JEANS SYMPHONY", "",""));
		db.Add(new Shop("AUCHAN <color=#787A79ff>Реконструкция</color>", "",""));
	}

	public List<Term> terms;

	void Start()
	{
		FillDatabase();
		
		Term t2017q4 = new Term();
		t2017q4.termName = "IV<size=60>кв.</size> 2017";
		t2017q4.shops = new List<string>();
		t2017q4.shops.Add("STRADIVARIUS");
		t2017q4.shops.Add("BANANA REPUBLIC");
		t2017q4.shops.Add("LACOSTE");
		t2017q4.shops.Add("IMAGINARIUM");
		t2017q4.shops.Add("X.O");
		t2017q4.shops.Add("McNEAL");
		t2017q4.shops.Add("CHRISTIAN BERG");
		t2017q4.shops.Add("MARKS & SPENCER");
		t2017q4.shops.Add("EUROSET");
		t2017q4.shops.Add("SONY");
		t2017q4.shops.Add("BORK");
		t2017q4.shops.Add("ACCESSORIES");
		t2017q4.shops.Add("CK JEANS");
		t2017q4.shops.Add("PANDORA");
		t2017q4.shops.Add("IGLA");
		t2017q4.shops.Add("GIPFEL");
		t2017q4.shops.Add("Дороги <color=#787A79ff>Реконструкция</color>");
		t2017q4.shops.Add("CUP <color=#787A79ff>Плиточные работы</color>");
		t2017q4.shops.Add("Ex-DECATHLON AREA <color=#787A79ff>Ремонт</color>");
		
		
		Term t2017q4y = new Term();
		t2017q4y.termName = "2018";
		t2017q4y.shops = new List<string>();
		t2017q4y.shops.Add("STRADIVARIUS");
		t2017q4y.shops.Add("BANANA REPUBLIC");
		t2017q4y.shops.Add("LACOSTE");
		t2017q4y.shops.Add("IMAGINARIUM");
		t2017q4y.shops.Add("X.O");
		t2017q4y.shops.Add("McNEAL");
		t2017q4y.shops.Add("CHRISTIAN BERG");
		t2017q4y.shops.Add("MARKS & SPENCER");
		t2017q4y.shops.Add("EUROSET");
		t2017q4y.shops.Add("SONY");
		t2017q4y.shops.Add("BORK");
		t2017q4y.shops.Add("ACCESSORIES");
		t2017q4y.shops.Add("CK JEANS");
		t2017q4y.shops.Add("PANDORA");
		t2017q4y.shops.Add("IGLA");
		t2017q4y.shops.Add("GIPFEL");
		t2017q4y.shops.Add("Дороги <color=#787A79ff>Реконструкция</color>");
		t2017q4y.shops.Add("CUP <color=#787A79ff>Плиточные работы</color>");
		t2017q4y.shops.Add("Ex-DECATHLON AREA <color=#787A79ff>Ремонт</color>");
		
		Term t2018q1 = new Term();
		t2018q1.termName = "I<size=60>кв.</size>  2018";
		t2018q1.shops = new List<string>();
		t2018q1.shops.Add("MANGO");
		t2018q1.shops.Add("CORSO COMO");
		t2018q1.shops.Add("MASCOTTE");
		t2018q1.shops.Add("SWAROVSKI");
		t2018q1.shops.Add("MAC");
		t2018q1.shops.Add("REEBOK");
		t2018q1.shops.Add("LUSIO");
		t2018q1.shops.Add("Дороги <color=#787A79ff>Реконструкция</color>");
		t2018q1.shops.Add("STOCKMAN <color=#787A79ff>Перепланировка</color>");
		
		Term t2018q2 = new Term();
		t2018q2.termName = "II<size=60>кв.</size>  2018";
		t2018q2.shops = new List<string>();
		t2018q2.shops.Add("KIKO");
		t2018q2.shops.Add("YVES ROCHER");
		t2018q2.shops.Add("ILE DE BEAUTE");
		t2018q2.shops.Add("MASCOTTE");
		t2018q2.shops.Add("REEBOK");
		t2018q2.shops.Add("REEBOK CLASSIC");
		t2018q2.shops.Add("5 KARMANOV");
		t2018q2.shops.Add("Дороги <color=#787A79ff>Реконструкция</color>");
		t2018q2.shops.Add("STOCKMAN <color=#787A79ff>Перепланировка</color>");
		
		Term t2018q3 = new Term();
		t2018q3.termName = "III<size=60>кв.</size>  2018";
		t2018q3.shops = new List<string>();
		t2018q3.shops.Add("YVES ROCHER");
		t2018q3.shops.Add("KIEHL’S");
		t2018q3.shops.Add("BOBBI BROWN");
		t2018q3.shops.Add("DE FACTO");
		t2018q3.shops.Add("MARMALATO");
		t2018q3.shops.Add("BUSTIER");
		t2018q3.shops.Add("LUSH");
		t2018q3.shops.Add("KIRA PLASTININA");
		t2018q3.shops.Add("ILE DE BEAUTE");
		t2018q3.shops.Add("MARKS & SPENCER");
		t2018q3.shops.Add("ALBLONE");
		t2018q3.shops.Add("MEZZATORRE");
		t2018q3.shops.Add("ECONICA");
		t2018q3.shops.Add("STRELLSON");
		t2018q3.shops.Add("ECCO ECCO KIDS");
		t2018q3.shops.Add("STOCKMAN <color=#787A79ff>Реконструкция</color>");
		t2018q3.shops.Add("<color=#787A79ff>Новый служебный коридор</color>");
		t2018q3.shops.Add("<color=#787A79ff>Перемещение коммунальных предприятий</color>");
		t2018q3.shops.Add("IKEA <color=#787A79ff>Реконструкция</color>");
		
		Term t2018q4 = new Term();
		t2018q4.termName = "IV<size=60>кв.</size>  2018";
		t2018q4.shops = new List<string>();
		t2018q4.shops.Add("ALBLONE");
		t2018q4.shops.Add("MEZZATORRE");
		t2018q4.shops.Add("ECONICA");
		t2018q4.shops.Add("STRELLSON");
		t2018q4.shops.Add("STOCKMAN <color=#787A79ff>Реконструкция</color>");
		t2018q4.shops.Add("<color=#787A79ff>Новый служебный коридор</color>");
		t2018q4.shops.Add("<color=#787A79ff>Перемещение коммунальных предприятий</color>");
		t2018q4.shops.Add("IKEA <color=#787A79ff>Реконструкция</color>");
		t2018q4.shops.Add("Парковка <color=#787A79ff>1 уровень</color>");
		
		Term t2018q4y = new Term();
		t2018q4y.termName = "2019";
		t2018q4y.shops = new List<string>();
		t2018q4y.shops.Add("ALBLONE");
		t2018q4y.shops.Add("MEZZATORRE");
		t2018q4y.shops.Add("ECONICA");
		t2018q4y.shops.Add("STRELLSON");
		t2018q4y.shops.Add("STOCKMAN <color=#787A79ff>Реконструкция</color>");
		t2018q4y.shops.Add("<color=#787A79ff>Новый служебный коридор</color>");
		t2018q4y.shops.Add("<color=#787A79ff>Перемещение коммунальных предприятий</color>");
		t2018q4y.shops.Add("IKEA <color=#787A79ff>Реконструкция</color>");
		t2018q4y.shops.Add("Парковка <color=#787A79ff>1 уровень</color>");
			
		
		Term t2019q1 = new Term();
		t2019q1.termName = "I<size=60>кв.</size>  2019";
		t2019q1.shops = new List<string>();
		t2019q1.shops.Add("ALBLONE");
		t2019q1.shops.Add("MEZZATORRE");
		t2019q1.shops.Add("ECONICA");
		t2019q1.shops.Add("STRELLSON");
		t2019q1.shops.Add("LAUREN VIDAL");
		t2019q1.shops.Add("U.S. POLO");
		t2019q1.shops.Add("GUESS");
		t2019q1.shops.Add("QUIKSILVER");
		t2019q1.shops.Add("LEGO");
		t2019q1.shops.Add("BERSHKA");
		t2019q1.shops.Add("SUPERSTEP");
		t2019q1.shops.Add("OYSHO");
		t2019q1.shops.Add("BENETTON");
		t2019q1.shops.Add("SCOTCH & SODA");
		t2019q1.shops.Add("VICTORIA SECRET");
		t2019q1.shops.Add("SAMSONITE");
		t2019q1.shops.Add("JEANS SYMPHONY");
		t2019q1.shops.Add("STOCKMAN <color=#787A79ff>Реконструкция</color>");
		t2019q1.shops.Add("<color=#787A79ff>Новый служебный коридор</color>");
		t2019q1.shops.Add("<color=#787A79ff>Перемещение коммунальных предприятий</color>");
		t2019q1.shops.Add("AUCHAN <color=#787A79ff>Реконструкция</color>");
		t2019q1.shops.Add("Парковка <color=#787A79ff>1 уровень</color>");
		
		termsList.Add(t2017q4);
		termsList.Add(t2017q4y);
		termsList.Add(t2018q1);
		termsList.Add(t2018q2);
		termsList.Add(t2018q3);
		termsList.Add(t2018q4);
		termsList.Add(t2018q4y);
		termsList.Add(t2019q1);
	}

	public void SetInfo(int number)
	{
		//Debug.Log(number);
		if (colomns.Length != 0)
		{
			foreach (GameObject go in colomns)
			{
				Destroy(go);
			}
		}
		termName.text = termsList[number].termName;
		int colomnsCount = termsList[number].shops.Count / 5;
		if (termsList[number].shops.Count % 5 != 0)
		{
			colomnsCount++;
		}
		colomns = new GameObject[colomnsCount];
		int k = 0;
		for (int i = 0; i < colomns.Length; i++)
		{
			GameObject col = Instantiate(colomn);
			colomns[i] = col;
			col.transform.SetParent(content.transform);
			col.GetComponent<RectTransform>().anchoredPosition = new Vector2(400*i, 0);
			col.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
			Colomn unfilledColomn = col.GetComponent<Colomn>();
			unfilledColomn.changesInfo = this;
			for (int j = 0; j < unfilledColomn.texts.Length; j++)
			{
				if (k < termsList[number].shops.Count)
				{
					unfilledColomn.texts[j].text = termsList[number].shops[k];
				}
				else
				{
					unfilledColomn.texts[j].gameObject.SetActive(false);
				}
				k++;
			}	
		}
		//Debug.Log(colomns.Length);
		if (colomns.Length < 4)
		{
			//Debug.Log(scrRect.enabled);
			scrRect.enabled = false;
			//Debug.Log(scrRect.enabled);
		}
		else
		{
			scrRect.enabled = true;
			content.GetComponent<RectTransform>().sizeDelta = new Vector2(400*colomns.Length, 400);
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown("c"))
		{
			SetInfo(UnityEngine.Random.Range(0, termsList.Count-1));
		}
	}
}
