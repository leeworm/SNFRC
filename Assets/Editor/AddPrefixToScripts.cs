using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class AddPrefixToScripts : EditorWindow
{
    private string folderPath = "Assets/HK_Folder/Scripts"; //  내 스크립트 폴더 경로
    private string prefix = "HK_"; //  붙이고 싶은 접두사

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

                // 클래스 이름 찾기
                Match classMatch = Regex.Match(fileContent, @"\b(public|internal)?\s*(partial\s*)?(class|struct|interface)\s+(\w+)");
                if (!classMatch.Success) continue;

                string originalClassName = classMatch.Groups[4].Value;
                string newClassName = prefix + originalClassName;

                // 클래스명 수정
                fileContent = Regex.Replace(fileContent, $@"\b{originalClassName}\b", newClassName);

                // 저장
                File.WriteAllText(filePath, fileContent);

                // 파일 이름도 변경
                string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), prefix + fileName);
                AssetDatabase.MoveAsset(filePath, newFilePath);
                Debug.Log($" Renamed: {fileName} → {prefix + fileName}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log(" 접두사 적용 완료!");
    }
}
