using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class AddPrefixToScripts : EditorWindow
{
    private string folderPath = "Assets/HK_Folder/Scripts"; //  �� ��ũ��Ʈ ���� ���
    private string prefix = "HK_"; //  ���̰� ���� ���λ�

    [MenuItem("Tools/Add Prefix To My Scripts")]
    public static void ShowWindow()
    {
        GetWindow<AddPrefixToScripts>("Add Prefix To Scripts");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField(" Add Prefix To Scripts", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Target Folder", folderPath);
        prefix = EditorGUILayout.TextField("Prefix", prefix);

        if (GUILayout.Button("Apply Prefix to .cs Files"))
        {
            ApplyPrefixToScripts();
        }
    }

    private void ApplyPrefixToScripts()
    {
        string[] files = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);

            if (!fileName.StartsWith(prefix))
            {
                string fileContent = File.ReadAllText(filePath);

                // Ŭ���� �̸� ã��
                Match classMatch = Regex.Match(fileContent, @"\b(public|internal)?\s*(partial\s*)?(class|struct|interface)\s+(\w+)");
                if (!classMatch.Success) continue;

                string originalClassName = classMatch.Groups[4].Value;
                string newClassName = prefix + originalClassName;

                // Ŭ������ ����
                fileContent = Regex.Replace(fileContent, $@"\b{originalClassName}\b", newClassName);

                // ����
                File.WriteAllText(filePath, fileContent);

                // ���� �̸��� ����
                string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), prefix + fileName);
                AssetDatabase.MoveAsset(filePath, newFilePath);
                Debug.Log($" Renamed: {fileName} �� {prefix + fileName}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log(" ���λ� ���� �Ϸ�!");
    }
}
