using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public Text GUIDText;
    public Text NameText;
    public InputField NameField;
    public Text NumberText;

    private string nameTest = string.Empty;
    private int numberTest = 0;

    private void Start()
    {
        GUIDText.text = CloudManager.i.UniqueIdentifier;
    }

    private void LateUpdate()
    {
        NameText.text = nameTest;
        NumberText.text = numberTest.ToString();
    }

    public void Set(string name)
    {
        nameTest = name;
    }

    public void Add()
    {
        numberTest += 1;
    }

    public void Minus()
    {
        numberTest -= 1;
    }

    public void Save()
    {
        AccountData data = new AccountData(nameTest, numberTest);
        CloudManager.i.Save(data.Serialize());
    }

    public void Load()
    {
        CloudManager.i.Load((result) =>
        {
            OnLoad(result);
        });
    }

    private void OnLoad(string json)
    {
        AccountData data = json.Deserialize();
        nameTest = data.Name;
        numberTest = data.Number;
    }
}
