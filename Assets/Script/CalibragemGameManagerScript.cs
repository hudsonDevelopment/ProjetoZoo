using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalibragemGameManagerScript : MonoBehaviour {

	public Camera cam;

	private SalvaDadosEntreScenes salvaDados;
	public GameObject ImageTarget;
	public GameObject CantosDobrado;
	public GameObject CantosEsticado;

	public ReadTarget imDetector;

	//private MessengerScript messenger;
	//private MessengerScript msn;

	private CalibradorScript calibrador;
	private bool exitBtn;

	private bool salvo;

    public Button calibrar;
    public Button reiniciar;

    public Transform posCanvas;

    public Text legenda;
    public TextAsset roteiro;

    public GMTutorialScript gerTexto;

    private bool testeTexto;
    private int passadas;
    public Text debug;
    // Use this for initialization
    void Start () {
        // Criando mensagiro inferior
        //calibrar.transform.SetPositionAndRotation(posCanvas.position,posCanvas.rotation);
        
		//messenger = gameObject.AddComponent<MessengerScript>();
		//messenger.InsereRectLinhas(0, Screen.height, Screen.width,2);

		//msn = gameObject.AddComponent<MessengerScript> ();
		//msn.InsereRect (new Rect(0,0,Screen.width, Screen.height/2.0f));

		calibrador = gameObject.AddComponent<CalibradorScript> ();
		calibrador.InsereCantos (CantosDobrado, CantosEsticado, ImageTarget);
		//calibrador.InsereMessenger (messenger);
		calibrador.InsereIMDetector (imDetector);
		calibrador.InsereCam (cam);

		salvo = false;
		exitBtn = false;

		// Criando componente para salvar os dados entre as scenes
		salvaDados = gameObject.AddComponent<SalvaDadosEntreScenes>();
		salvaDados.InsereCantos (CantosDobrado, CantosEsticado);
	}

	// Update is called once per frame
	void Update () {
        if (exitBtn)
        {
            voltarMenuPrincipal();
        }
		
		if (!salvo && calibrador.EstaCalibrado ()) {
			salvaDados.salvarCalibragem ();
			salvo = true;
		}


		//DEBUGANDO ();
	}

	/*void OnGUI(){
		exitBtn = GUI.RepeatButton (new Rect (Screen.width - 100.0f, Screen.height - 100.0f, 100.0f, 100.0f), "<b>Sair</b>");
	}*/

	void voltarMenuPrincipal(){
		SceneManager.LoadSceneAsync ("menuInicial");
	}

	/*void DEBUGANDO(){
		msn.messengerTxt = "<color=magenta>" +
			ImageTarget.transform.GetChild (0).transform.position+
			ImageTarget.transform.GetChild (1).transform.position+
			"</color>";
	}*/
    public void clickSair()
    {
        exitBtn = true;
    }
    public void clickCalibrar()
    {
        calibrador.setCalibrarBtn(true);
        //Debug.Log("Calibrar clicado");
    }
    public void clickReiniciar()
    {
        calibrador.setReiniciarBtn(true);
        //Debug.Log("Reiniciar clicado");
    }
    public void passaFala()
    {
        string textoAnt;
        string textoDep;
        if (passadas <= 6 || passadas>7 && passadas <12 || passadas >12)
        {
            textoAnt = legenda.text.ToString();
            if (passadas == 7)
            {
                if (calibrador.getDefinido1())
                {
                    gerTexto.passaTexto();
                }
            }
            else if (passadas == 12)
            {
                if (calibrador.getDefinido2())
                {
                    gerTexto.passaTexto();
                }
            }
            else
            {
                gerTexto.passaTexto();
                //textoDep = gerTexto.textoFalas.ToString();
            }
            if (conferirSePassou(textoAnt))
            {
                passadas++;
            }
        }
        debug.text = passadas.ToString();
    }
    public bool conferirSePassou(string texto1)
    {
        return string.Compare(texto1, legenda.text.ToString(), true) != 0;
    }
}
