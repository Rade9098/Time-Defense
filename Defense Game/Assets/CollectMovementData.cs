using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.IO;
//using UnityEngine.SceneManagement;


public class CollectMovementData : MonoBehaviour
{
    public bool gameIs2D = false;
    public bool enableUI = true;
    public bool recordKeys = true;
    public bool recordCameraMovements = true;
    public bool recordObjectsViewed = true;
    public bool recordMouseBehaviors = true;
    public bool recordControllerInput = true; //will only collect controller input if 1 controller is connected at the start of a level.
    public bool persistRecorderThroughLevels = true;

    //log locations
    public string cameraPosistionsLogLocation = "CameraPositions";
    public string cameraRotationsLogLocation = "CameraRotations";
    public string objectsLookedAtLogLocation = "ObjectsLookedAtCount";
    public string keysPressedLogLocation = "ButtonsPressed";
    public string mouseScreenPosistionsLogLocation = "MouseScreenPositions";
    public string mouseWorldPositionsLogLocation = "MouseWorldPositions";
    public string controllerInputLogLocation = "ControllerInput";
    public string combinedActionsLogLocation = "CombinedActions";
    public string logDirectoryPath = "";
    public string fileType = ".txt"; //if you change this from csv, files will no longer work with excel
    public bool prependLevelNameToLogLocation = true; //this will enable prepending of level name to save locations

    //for 3d games, if you don't want objects looked at to be straight in front of your main camera enable this option, and provide tag names for your objects.
    public bool should3DGameUseDifferentLookDirection = false;

    private GameObject rayCastForwardDirection;
    private GameObject mainCharacter;

    //should be a tag indicating the origin object for raycasting and a child object of your main character that represents the forward diretion for the character.
    public string mainCharacterTagName;
    public string rayCastDirectionTransformTagName;

    private List<Vector3> cameraPositions = new List<Vector3>();
    private List<Quaternion> cameraRotations = new List<Quaternion>();
    private List<Vector3> mouseScreenPositions = new List<Vector3>();
    private List<Vector3> mouseWorldPositions = new List<Vector3>();
    private string everyKeyPressed = "";
    private string everyControllerKeyPressed = "";
    private Transform child;
    private bool recordingMovement = true;
    private int recordedFrame = 0;
    public Camera replayCamera;
    public string targetCameraTag = "MainCamera";
    private Camera mainCamera;
    private Dictionary<string, int> dictionary;
    private Dictionary<float, string> combinedActions;
    private Dictionary<int, string> playBackActions;
    private float elapsedMilliseconds;
    private StringBuilder combinedActionsBuilder;
    private int elapsedFrames;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(targetCameraTag).GetComponent<Camera>();
        replayCamera.enabled = false;
        child = gameObject.transform.GetChild(0);
        dictionary = new Dictionary<string, int>();
        combinedActions = new Dictionary<float, string>();
        playBackActions = new Dictionary<int, string>();
        elapsedMilliseconds = 0;
        elapsedFrames = 0;
        combinedActionsBuilder = new StringBuilder();

        if (gameIs2D || should3DGameUseDifferentLookDirection)
        {
            mainCharacter = GameObject.FindGameObjectWithTag(mainCharacterTagName);
            rayCastForwardDirection = GameObject.FindGameObjectWithTag(rayCastDirectionTransformTagName);
        }
        else
        {
            mainCharacter = null;
            rayCastForwardDirection = null;
        }

        if (prependLevelNameToLogLocation)
        {
            //string logLocationsPrepend = SceneManager.GetActiveScene().name;
            //if (!logDirectoryPath.Equals(""))
            //{
            //    logLocationsPrepend = logDirectoryPath + "/" + logLocationsPrepend;
            //}
            //cameraPosistionsLogLocation = logLocationsPrepend + "-" + cameraPosistionsLogLocation + fileType;
            //cameraRotationsLogLocation = logLocationsPrepend + "-" + cameraRotationsLogLocation + fileType;
            //objectsLookedAtLogLocation = logLocationsPrepend + "-" + objectsLookedAtLogLocation + fileType;
            //keysPressedLogLocation = logLocationsPrepend + "-" + keysPressedLogLocation + fileType;
            //mouseWorldPositionsLogLocation = logLocationsPrepend + "-" + mouseWorldPositionsLogLocation + fileType;
            //mouseScreenPosistionsLogLocation = logLocationsPrepend + "-" + mouseScreenPosistionsLogLocation + fileType;
            //controllerInputLogLocation = logLocationsPrepend + "-" + controllerInputLogLocation + fileType;
            //combinedActionsLogLocation = logLocationsPrepend + "-" + combinedActionsLogLocation + fileType;
        }

        if (persistRecorderThroughLevels)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (gameIs2D && should3DGameUseDifferentLookDirection)
        {
            Debug.Log("Game can't be both 2D and 3D, check gameIs2D and should3DGameUseDifferentLookDirection to see which one you really want");
            gameIs2D = false;
        }

        if (!(recordControllerInput && Input.GetJoystickNames().Length == 1))
        {
            recordControllerInput = false;
        }
    }

    void Update()
    {
        elapsedMilliseconds += Time.deltaTime;
        elapsedFrames++;
        combinedActionsBuilder.Length = 0;

        if (Input.GetJoystickNames().Length != 1)
        {
            recordControllerInput = false;
        }

        if (recordKeys)
        {
            RecordKeysPressed();
        }

        if (recordMouseBehaviors)
        {
            RecordMouseBehaviors(gameIs2D);
        }

        if (recordControllerInput)
        {
            RecordControllerInput();
        }

        if (recordObjectsViewed)
        {
            RecordViewedObjects(gameIs2D);
        }

        if (recordCameraMovements)
        {
            RecordMovement();
        }

        if (combinedActionsBuilder.Length > 0)
        {
            combinedActions.Add(elapsedMilliseconds, combinedActionsBuilder.ToString());
        }

        if(!playBackActions.ContainsKey(elapsedFrames))
        {
            playBackActions.Add(elapsedFrames, combinedActionsBuilder.ToString());

        }
    }

    private void RecordControllerInput()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i.ToString()))
            {
                string toAdd = i.ToString();
                if (everyControllerKeyPressed.Equals(""))
                {
                    everyControllerKeyPressed = toAdd;
                }
                else
                {
                    everyControllerKeyPressed = everyControllerKeyPressed + "," + toAdd;
                }

                combinedActionsBuilder.Append("\tController: " + toAdd.Trim(','));
            }
        }
    }

    private void RecordMouseBehaviors(bool isGame2D)
    {
        mouseScreenPositions.Add(Input.mousePosition);
        combinedActionsBuilder.Append("\tMouseScreenPoint: " + Input.mousePosition.ToString());

        if (!isGame2D)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mouseWorldPositions.Add(hit.point);
                combinedActionsBuilder.Append("\tMouse World Point: " + hit.point.ToString());
            }
        }
    }

    //I promise this isn't a malicious logger any government agency looking at this, its just for game testing
    //Unity doesn't like to pass values for inputString for certain buttons (non alpha-numeric) so they have to be logged manually. 
    private void RecordKeysPressed()
    {
        if (Input.anyKey)
        {
            string toAdd = "";
            if (Input.GetKey(KeyCode.Space))
            {
                toAdd = "space";
            }
            else if (Input.GetKey(KeyCode.Tab))
            {
                toAdd = "tab";
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                toAdd = "leftShift";
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                toAdd = "escape";
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                toAdd = "leftControl";
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                toAdd = "leftAlt";
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                toAdd = "mouseLeft";
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                toAdd = "mouseRight";
            }
            else if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                toAdd = "mouseMiddle";
            }
            else if (!Input.inputString.Equals(""))
            {
                toAdd = Input.inputString;
            }

            if (!toAdd.Equals(""))
            {
                if (everyKeyPressed.Equals(""))
                {
                    everyKeyPressed = toAdd;
                }
                else
                {
                    toAdd = "," + toAdd;
                    everyKeyPressed = everyKeyPressed + toAdd;
                }

                combinedActionsBuilder.Append("\tInput: " + toAdd.Trim(','));
            }
        }
    }

    private void RecordViewedObjects(bool is2dGame)
    {
        if (!is2dGame)
        {
            RaycastHit[] hits = Physics.RaycastAll((should3DGameUseDifferentLookDirection) ? mainCharacter.transform.position : mainCamera.transform.position,
                (should3DGameUseDifferentLookDirection) ? rayCastForwardDirection.transform.position : mainCamera.transform.TransformDirection(Vector3.forward), 100f);

            foreach (RaycastHit h in hits)
            {
                if (!h.transform.gameObject.name.Equals("FPSController") && !h.transform.gameObject.CompareTag("MainCamera") && !h.transform.gameObject.CompareTag("Camera") && !h.transform.gameObject.name.Equals("Terrain"))
                {
                    if (dictionary.ContainsKey(h.transform.gameObject.name))
                    {
                        dictionary[h.transform.gameObject.name] += 1;
                    }
                    else
                    {
                        dictionary.Add(h.transform.gameObject.name, 1);
                    }
                }
            }
        }
        else if (is2dGame)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(mainCharacter.transform.position, rayCastForwardDirection.transform.position, 100f);
            foreach (RaycastHit2D h in hits)
            {
                if (!h.transform.gameObject.name.Equals("FPSController") && !h.transform.gameObject.CompareTag("MainCamera") && !h.transform.gameObject.CompareTag("Camera") && !h.transform.gameObject.name.Equals("Terrain"))
                {
                    if (dictionary.ContainsKey(h.transform.gameObject.name))
                    {
                        dictionary[h.transform.gameObject.name] += 1;
                    }
                    else
                    {
                        dictionary.Add(h.transform.gameObject.name, 1);
                    }
                }
            }
        }
    }

    private void RecordMovement()
    {
        if (recordingMovement)
        {
            cameraPositions.Add(mainCamera.transform.position);
            cameraRotations.Add(mainCamera.transform.rotation);
        }
        else
        {
            PlayBackMovement();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            cameraPositions.Clear();
            cameraRotations.Clear();
            recordingMovement = true;
            mainCamera.enabled = true;
            replayCamera.enabled = false;
            elapsedFrames = 0;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            recordingMovement = false;
            recordedFrame = 0;
            elapsedFrames = 0;
            mainCamera.enabled = false;
            replayCamera.enabled = true;
        }
    }

    private void PlayBackMovement()
    {
        if (recordedFrame < cameraPositions.Count)
        {
            replayCamera.transform.position = (Vector3)cameraPositions[recordedFrame];
            Quaternion temp = (Quaternion)cameraRotations[recordedFrame];
            replayCamera.transform.eulerAngles = temp.eulerAngles;
            Debug.Log(playBackActions[recordedFrame]);
            recordedFrame++;
        }
        else
        {
            recordingMovement = true;
            mainCamera.enabled = true;
            replayCamera.enabled = false;
        }
    }

    private void SaveCameraMovements(bool is2dGame)
    {
        using (StreamWriter file = new StreamWriter(cameraPosistionsLogLocation))
        {
            foreach (var entry in cameraPositions)
            {
                file.WriteLine("[{0}]", entry);
            }
        }

        if (!is2dGame)
        {
            using (StreamWriter file = new StreamWriter(cameraRotationsLogLocation))
            {
                foreach (Quaternion entry in cameraRotations)
                {
                    var r = Quaternion.ToEulerAngles(entry);
                    file.WriteLine("[{0}]", r);
                }
            }
        }
    }

    private void SaveObjectsLookedAt()
    {
        using (StreamWriter file = new StreamWriter(objectsLookedAtLogLocation))
        {
            foreach (var entry in dictionary)
            {
                file.WriteLine("[{0} {1}]", entry.Key, entry.Value);
            }
        }
    }

    private void SaveKeysPressed()
    {
        using (StreamWriter file = new StreamWriter(keysPressedLogLocation))
        {
            file.WriteLine(everyKeyPressed);
        }
    }

    private void SaveMouseBehaviors(bool isGame2D)
    {
        using (StreamWriter file = new StreamWriter(mouseScreenPosistionsLogLocation))
        {
            foreach (var entry in mouseScreenPositions)
            {
                file.WriteLine("[{0}]", entry);
            }
        }

        if (!isGame2D)
        {
            using (StreamWriter file = new StreamWriter(mouseWorldPositionsLogLocation))
            {
                foreach (var entry in mouseWorldPositions)
                {
                    file.WriteLine("[{0}]", entry);
                }
            }
        }
    }

    private void SaveControllerInput()
    {
        using (StreamWriter file = new StreamWriter(controllerInputLogLocation))
        {
            file.WriteLine(everyControllerKeyPressed);
        }
    }

    private void SaveCombinedActions()
    {
        using (StreamWriter file = new StreamWriter(combinedActionsLogLocation))
        {
            foreach (var entry in combinedActions)
            {
                file.WriteLine("[{0} {1}]", entry.Key, entry.Value);
            }
        }
    }

    void OnDisable()
    {
        if (recordCameraMovements)
        {
            SaveCameraMovements(gameIs2D);
        }

        if (recordObjectsViewed)
        {
            SaveObjectsLookedAt();
        }

        if (recordKeys)
        {
            SaveKeysPressed();
        }

        if (recordMouseBehaviors)
        {
            SaveMouseBehaviors(gameIs2D);
        }

        if (recordControllerInput)
        {
            SaveControllerInput();
        }

        SaveCombinedActions();
    }
}
