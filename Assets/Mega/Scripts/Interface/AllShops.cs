using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllShops : MonoBehaviour {

	public GameObject[] colomns;
	public GameObject colomn;
	public GameObject content;
	public ScrollRect scrRect;
	public List<ChangesInfo.Shop> db = new List<ChangesInfo.Shop>();
	public Table list;
	public Table shop;
	public ShopTable shopTable;
	public ChangesInfo changesInfo;
	
	private string temporaryString = "В данном периоде реконструкции на Вашем объекте будут произведены технические работы. К первому числу указанного квартала Вам необходимо освободить объект";
	
	
	public void SetShop(string name)
	{
		ChangesInfo.Shop newShop = db.Find(x => x.name == name);
		Debug.Log(name+" "+newShop+" "+newShop.name);
		shopTable.shopName.text = newShop.name;
		if (newShop.description =="")
			shopTable.shopDescription.text = temporaryString;
		//shopTable.icon
		list.RollOut();
		shop.RollIn();
	}
	
	
	// Use this for initialization
void Start () {
		//db.Add(new ChangesInfo.Shop("db.Add(new ChangesInfo.Shop("STRADIVARIUS", "",""));
db.Add(new ChangesInfo.Shop("1C: Interes", "",""));
db.Add(new ChangesInfo.Shop("5 karmaNov", "",""));
db.Add(new ChangesInfo.Shop("Adidas Kids", "",""));
db.Add(new ChangesInfo.Shop("Albione", "",""));
db.Add(new ChangesInfo.Shop("Aldo", "",""));
db.Add(new ChangesInfo.Shop("Armani Exchange", "",""));
db.Add(new ChangesInfo.Shop("ASUS", "",""));
db.Add(new ChangesInfo.Shop("Benetton", "",""));
db.Add(new ChangesInfo.Shop("Bershka", "",""));
db.Add(new ChangesInfo.Shop("Betkhoven", "",""));
db.Add(new ChangesInfo.Shop("BORK", "",""));
db.Add(new ChangesInfo.Shop("Calvin Klein Jeans", "",""));
db.Add(new ChangesInfo.Shop("Calzedonia", "",""));
db.Add(new ChangesInfo.Shop("Carlo Pazolini", "",""));
db.Add(new ChangesInfo.Shop("Caterina Leman", "",""));
db.Add(new ChangesInfo.Shop("CHANEL", "",""));
db.Add(new ChangesInfo.Shop("Chester", "",""));
db.Add(new ChangesInfo.Shop("Christian Berg", "",""));
db.Add(new ChangesInfo.Shop("CINNABON", "",""));
db.Add(new ChangesInfo.Shop("Consul", "",""));
db.Add(new ChangesInfo.Shop("Contrast", "",""));
db.Add(new ChangesInfo.Shop("Correa`s", "",""));
db.Add(new ChangesInfo.Shop("Crocs", "",""));
db.Add(new ChangesInfo.Shop("Detsky Mir", "",""));
db.Add(new ChangesInfo.Shop("DIM SUM", "",""));
db.Add(new ChangesInfo.Shop("DKNY", "",""));
db.Add(new ChangesInfo.Shop("Ecco, Ecco Kids", "",""));
db.Add(new ChangesInfo.Shop("Econika", "",""));
db.Add(new ChangesInfo.Shop("Egoist", "",""));
db.Add(new ChangesInfo.Shop("Estelle Adony", "",""));
db.Add(new ChangesInfo.Shop("Eterna", "",""));
db.Add(new ChangesInfo.Shop("Euroset", "",""));
db.Add(new ChangesInfo.Shop("Falconeri", "",""));
db.Add(new ChangesInfo.Shop("Falke", "",""));
db.Add(new ChangesInfo.Shop("Finn Flare", "",""));
db.Add(new ChangesInfo.Shop("Furla", "",""));
db.Add(new ChangesInfo.Shop("GAGAWA", "",""));
db.Add(new ChangesInfo.Shop("Gelatissimo/Frank & Friends", "",""));
db.Add(new ChangesInfo.Shop("Gelatissimo/Frank & Friends", "",""));
db.Add(new ChangesInfo.Shop("Gloria Jeans", "",""));
db.Add(new ChangesInfo.Shop("GUESS", "",""));
db.Add(new ChangesInfo.Shop("Gulliver", "",""));
db.Add(new ChangesInfo.Shop("Happy Steps", "",""));
db.Add(new ChangesInfo.Shop("Helmige", "",""));
db.Add(new ChangesInfo.Shop("Henderson", "",""));
db.Add(new ChangesInfo.Shop("Hogl", "",""));
db.Add(new ChangesInfo.Shop("Intimissimi", "",""));
db.Add(new ChangesInfo.Shop("IQTOY Pravilnie igrushki", "",""));
db.Add(new ChangesInfo.Shop("Jo Malone", "",""));
db.Add(new ChangesInfo.Shop("KANZ", "",""));
db.Add(new ChangesInfo.Shop("KANZLER", "",""));
db.Add(new ChangesInfo.Shop("Kinostar", "",""));
db.Add(new ChangesInfo.Shop("Kofeynaya Kantata", "",""));
db.Add(new ChangesInfo.Shop("KRISPY KREME", "",""));
db.Add(new ChangesInfo.Shop("Kroshka Kartoshka", "",""));
db.Add(new ChangesInfo.Shop("Lacoste", "",""));
db.Add(new ChangesInfo.Shop("Lady & Gentleman City", "",""));
db.Add(new ChangesInfo.Shop("LAUREN VIDAL", "",""));
db.Add(new ChangesInfo.Shop("LAVKALAVKA", "",""));
db.Add(new ChangesInfo.Shop("LEGO", "",""));
db.Add(new ChangesInfo.Shop("Lensmaster", "",""));
db.Add(new ChangesInfo.Shop("Levi's", "",""));
db.Add(new ChangesInfo.Shop("L'Occitane", "",""));
db.Add(new ChangesInfo.Shop("LUSIO", "",""));
db.Add(new ChangesInfo.Shop("MAC", "",""));
db.Add(new ChangesInfo.Shop("Marella", "",""));
db.Add(new ChangesInfo.Shop("Marks&Spencer", "",""));
db.Add(new ChangesInfo.Shop("Marks&Spencer", "",""));
db.Add(new ChangesInfo.Shop("Mascotte", "",""));
db.Add(new ChangesInfo.Shop("Massimo Dutti", "",""));
db.Add(new ChangesInfo.Shop("McNeal", "",""));
db.Add(new ChangesInfo.Shop("Menza", "",""));
db.Add(new ChangesInfo.Shop("Meucci", "",""));
db.Add(new ChangesInfo.Shop("Mezzatore", "",""));
db.Add(new ChangesInfo.Shop("Michaela", "",""));
db.Add(new ChangesInfo.Shop("Mlesna", "",""));
db.Add(new ChangesInfo.Shop("Moscow Jewelry Factory", "",""));
db.Add(new ChangesInfo.Shop("Mothercare", "",""));
db.Add(new ChangesInfo.Shop("Musetti", "",""));
db.Add(new ChangesInfo.Shop("Musetti", "",""));
db.Add(new ChangesInfo.Shop("Neopharm", "",""));
db.Add(new ChangesInfo.Shop("NIKA", "",""));
db.Add(new ChangesInfo.Shop("Nike", "",""));
db.Add(new ChangesInfo.Shop("ObedBufet", "",""));
db.Add(new ChangesInfo.Shop("Oysho", "",""));
db.Add(new ChangesInfo.Shop("Papa John`s", "",""));
db.Add(new ChangesInfo.Shop("Pennyblack", "",""));
db.Add(new ChangesInfo.Shop("Pho Bo", "",""));
db.Add(new ChangesInfo.Shop("Photosphera", "",""));
db.Add(new ChangesInfo.Shop("Planeta Kolgotok", "",""));
db.Add(new ChangesInfo.Shop("QuikSilver", "",""));
db.Add(new ChangesInfo.Shop("re:Store", "",""));
db.Add(new ChangesInfo.Shop("RED MANGO", "",""));
db.Add(new ChangesInfo.Shop("Reebok", "",""));
db.Add(new ChangesInfo.Shop("Reebok Classic", "",""));
db.Add(new ChangesInfo.Shop("ReFresh", "",""));
db.Add(new ChangesInfo.Shop("Reima", "",""));
db.Add(new ChangesInfo.Shop("Rendez-Vous", "",""));
db.Add(new ChangesInfo.Shop("Reserved", "",""));
db.Add(new ChangesInfo.Shop("Rive Gauche", "",""));
db.Add(new ChangesInfo.Shop("Samsung", "",""));
db.Add(new ChangesInfo.Shop("Scotch & Soda", "",""));
db.Add(new ChangesInfo.Shop("Shvilli", "",""));
db.Add(new ChangesInfo.Shop("SKOROMAMA", "",""));
db.Add(new ChangesInfo.Shop("Sony", "",""));
db.Add(new ChangesInfo.Shop("Sportmaster", "",""));
db.Add(new ChangesInfo.Shop("Starbucks", "",""));
db.Add(new ChangesInfo.Shop("Strellson", "",""));
db.Add(new ChangesInfo.Shop("SUBWAY", "",""));
db.Add(new ChangesInfo.Shop("SUNLIGHT", "",""));
db.Add(new ChangesInfo.Shop("SuperStep", "",""));
db.Add(new ChangesInfo.Shop("Swarovski", "",""));
db.Add(new ChangesInfo.Shop("Swatch", "",""));
db.Add(new ChangesInfo.Shop("TEREMOK", "",""));
db.Add(new ChangesInfo.Shop("Timberland", "",""));
db.Add(new ChangesInfo.Shop("Triumph", "",""));
db.Add(new ChangesInfo.Shop("U.S.Polo ASSN", "",""));
db.Add(new ChangesInfo.Shop("UNIQLO", "",""));
db.Add(new ChangesInfo.Shop("Upside Down Cake", "",""));
db.Add(new ChangesInfo.Shop("Vacant", "",""));
db.Add(new ChangesInfo.Shop("Vacant", "",""));
db.Add(new ChangesInfo.Shop("Vacant", "",""));
db.Add(new ChangesInfo.Shop("Vacant", "",""));
db.Add(new ChangesInfo.Shop("Vagabond", "",""));
db.Add(new ChangesInfo.Shop("Vangold", "",""));
db.Add(new ChangesInfo.Shop("Vans", "",""));
db.Add(new ChangesInfo.Shop("Vash razmer", "",""));
db.Add(new ChangesInfo.Shop("Wokker", "",""));
db.Add(new ChangesInfo.Shop("Yu Kids Island", "",""));
db.Add(new ChangesInfo.Shop("ZARA", "",""));
db.Add(new ChangesInfo.Shop("ZARA", "",""));
db.Add(new ChangesInfo.Shop("Zara Home", "",""));

	db.ForEach(x=> x.name = x.name.ToUpper());
	SetInfo();
}
	
	public void SetInfo()
	{
		//Debug.Log(number);
		if (colomns.Length != 0)
		{
			foreach (GameObject go in colomns)
			{
				Destroy(go);
			}
		}
		//termName.text = termsList[number].termName;
		int colomnsCount = db.Count / 5;
		if (db.Count % 5 != 0)
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
			unfilledColomn.changesInfo = changesInfo;
			for (int j = 0; j < unfilledColomn.texts.Length; j++)
			{
				if (k < db.Count)
				{
					unfilledColomn.texts[j].text = db[k].name;
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
	
	// Update is called once per frame
	void Update () {
		
	}
}