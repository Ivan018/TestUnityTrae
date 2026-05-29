using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using TestUnityTrae.UI;

namespace TestUnityTrae.Editor
{
    public static class LoginUIBuilder
    {
        private static TMP_FontAsset m_defaultFont;

        [MenuItem("Tools/Generate Login UI")]
        public static void GenerateLoginUI()
        {
            m_defaultFont = TMP_Settings.defaultFontAsset;

            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            Transform existingRoot = canvas.transform.Find("LoginRoot");
            if (existingRoot != null)
            {
                Object.DestroyImmediate(existingRoot.gameObject);
            }

            GameObject rootPanel = new GameObject("LoginRoot");
            rootPanel.transform.SetParent(canvas.transform);

            RectTransform rootRect = rootPanel.AddComponent<RectTransform>();
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            Image rootImage = rootPanel.AddComponent<Image>();
            rootImage.color = Color.white;

            CreateLeftPanel(rootPanel.transform);
            GameObject rightPanel = CreateRightPanel(rootPanel.transform);

            LoginController controller = canvas.gameObject.GetComponent<LoginController>();
            if (controller == null)
            {
                controller = canvas.gameObject.AddComponent<LoginController>();
            }

            TMP_InputField accountInput = rightPanel.transform.Find("LoginCard/AccountInput").GetComponent<TMP_InputField>();
            TMP_InputField passwordInput = rightPanel.transform.Find("LoginCard/PasswordInput").GetComponent<TMP_InputField>();
            Toggle rememberToggle = rightPanel.transform.Find("LoginCard/RememberToggle").GetComponent<Toggle>();
            Button loginButton = rightPanel.transform.Find("LoginCard/LoginButton").GetComponent<Button>();

            SerializedObject serialController = new SerializedObject(controller);
            serialController.FindProperty("input_account").objectReferenceValue = accountInput;
            serialController.FindProperty("input_password").objectReferenceValue = passwordInput;
            serialController.FindProperty("toggle_remember").objectReferenceValue = rememberToggle;
            serialController.FindProperty("btn_login").objectReferenceValue = loginButton;
            serialController.ApplyModifiedProperties();

            Selection.activeGameObject = canvas.gameObject;
            Debug.Log("Login UI generated successfully!");
        }

        private static void CreateLeftPanel(Transform parent)
        {
            GameObject panel = new GameObject("LeftPanel");
            panel.transform.SetParent(parent);

            RectTransform rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0.55f, 1);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Image image = panel.AddComponent<Image>();
            image.color = new Color32(37, 99, 235, 255);

            GameObject content = new GameObject("Content");
            content.transform.SetParent(panel.transform);

            RectTransform contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.pivot = new Vector2(0.5f, 0.5f);
            contentRect.localPosition = Vector3.zero;
            contentRect.sizeDelta = new Vector2(200, 200);

            GameObject logo = new GameObject("Logo");
            logo.transform.SetParent(content.transform);

            RectTransform logoRect = logo.AddComponent<RectTransform>();
            logoRect.anchorMin = new Vector2(0.5f, 1);
            logoRect.anchorMax = new Vector2(0.5f, 1);
            logoRect.pivot = new Vector2(0.5f, 1);
            logoRect.localPosition = new Vector3(0, 0, 0);
            logoRect.sizeDelta = new Vector2(120, 120);

            Image logoImage = logo.AddComponent<Image>();
            logoImage.color = new Color(1, 1, 1, 0.2f);

            GameObject brandName = new GameObject("BrandName");
            brandName.transform.SetParent(content.transform);

            RectTransform brandNameRect = brandName.AddComponent<RectTransform>();
            brandNameRect.anchorMin = new Vector2(0.5f, 0.3f);
            brandNameRect.anchorMax = new Vector2(0.5f, 0.3f);
            brandNameRect.pivot = new Vector2(0.5f, 0.5f);
            brandNameRect.localPosition = new Vector3(0, 0, 0);
            brandNameRect.sizeDelta = new Vector2(200, 50);

            TextMeshProUGUI brandText = brandName.AddComponent<TextMeshProUGUI>();
            brandText.text = "Wonder Space";
            brandText.fontSize = 42;
            brandText.fontStyle = FontStyles.Bold;
            brandText.color = Color.white;
            brandText.alignment = TextAlignmentOptions.Center;
            if (m_defaultFont != null) brandText.font = m_defaultFont;

            GameObject brandDesc = new GameObject("BrandDesc");
            brandDesc.transform.SetParent(content.transform);

            RectTransform brandDescRect = brandDesc.AddComponent<RectTransform>();
            brandDescRect.anchorMin = new Vector2(0.5f, 0.1f);
            brandDescRect.anchorMax = new Vector2(0.5f, 0.1f);
            brandDescRect.pivot = new Vector2(0.5f, 0.5f);
            brandDescRect.localPosition = new Vector3(0, 0, 0);
            brandDescRect.sizeDelta = new Vector2(200, 30);

            TextMeshProUGUI descText = brandDesc.AddComponent<TextMeshProUGUI>();
            descText.text = "编辑平台";
            descText.fontSize = 16;
            descText.color = new Color(1, 1, 1, 0.8f);
            descText.alignment = TextAlignmentOptions.Center;
            if (m_defaultFont != null) descText.font = m_defaultFont;
        }

        private static GameObject CreateRightPanel(Transform parent)
        {
            GameObject panel = new GameObject("RightPanel");
            panel.transform.SetParent(parent);

            RectTransform rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.55f, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Image image = panel.AddComponent<Image>();
            image.color = new Color32(248, 250, 252, 255);

            GameObject loginCard = new GameObject("LoginCard");
            loginCard.transform.SetParent(panel.transform);

            RectTransform cardRect = loginCard.AddComponent<RectTransform>();
            cardRect.anchorMin = new Vector2(0.5f, 0.5f);
            cardRect.anchorMax = new Vector2(0.5f, 0.5f);
            cardRect.pivot = new Vector2(0.5f, 0.5f);
            cardRect.localPosition = Vector3.zero;
            cardRect.sizeDelta = new Vector2(400, 420);

            Image cardImage = loginCard.AddComponent<Image>();
            cardImage.color = Color.white;

            GameObject title = new GameObject("Title");
            title.transform.SetParent(loginCard.transform);

            RectTransform titleRect = title.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.5f);
            titleRect.anchorMax = new Vector2(0.5f, 0.5f);
            titleRect.pivot = new Vector2(0.5f, 0.5f);
            titleRect.localPosition = Vector3.zero;
            titleRect.sizeDelta = new Vector2(300, 40);
            titleRect.anchoredPosition = new Vector2(0, 140);

            TextMeshProUGUI titleText = title.AddComponent<TextMeshProUGUI>();
            titleText.text = "欢迎登录";
            titleText.fontSize = 28;
            titleText.fontStyle = FontStyles.Bold;
            titleText.color = new Color32(30, 41, 59, 255);
            titleText.alignment = TextAlignmentOptions.Center;
            if (m_defaultFont != null) titleText.font = m_defaultFont;

            GameObject accountInput = CreateInputField("AccountInput", "请输入账号", false, loginCard.transform);
            accountInput.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);

            GameObject passwordInput = CreateInputField("PasswordInput", "请输入密码", true, loginCard.transform);
            passwordInput.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);

            GameObject rememberToggle = CreateToggle("RememberToggle", "记住账号密码", loginCard.transform);
            rememberToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160, -100);

            GameObject loginButton = CreateButton("LoginButton", "登录", loginCard.transform);
            loginButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -170);

            return panel;
        }

        private static GameObject CreateInputField(string name, string placeholder, bool isPassword, Transform parent)
        {
            GameObject inputObj = new GameObject(name);
            inputObj.transform.SetParent(parent);

            RectTransform rect = inputObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(320, 48);

            Image bgImage = inputObj.AddComponent<Image>();
            bgImage.color = new Color32(248, 250, 252, 255);

            Image borderImage = new GameObject("Border").AddComponent<Image>();
            borderImage.transform.SetParent(inputObj.transform);
            borderImage.rectTransform.anchorMin = Vector2.zero;
            borderImage.rectTransform.anchorMax = Vector2.one;
            borderImage.rectTransform.offsetMin = Vector2.zero;
            borderImage.rectTransform.offsetMax = Vector2.zero;
            borderImage.color = new Color32(226, 232, 240, 255);

            TMP_InputField input = inputObj.AddComponent<TMP_InputField>();
            if (isPassword)
            {
                input.contentType = TMP_InputField.ContentType.Password;
            }
            input.placeholder = CreatePlaceholderText(inputObj.transform, placeholder);
            input.textComponent = CreateTextComponent(inputObj.transform);
            input.transition = Selectable.Transition.ColorTint;
            input.targetGraphic = bgImage;
            input.colors = GetInputFieldColorBlock();

            return inputObj;
        }

        private static TextMeshProUGUI CreatePlaceholderText(Transform parent, string text)
        {
            GameObject placeholder = new GameObject("Placeholder");
            placeholder.transform.SetParent(parent);

            RectTransform rect = placeholder.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.pivot = new Vector2(0, 0.5f);
            rect.offsetMin = new Vector2(16, 0);
            rect.offsetMax = new Vector2(-16, 0);

            TextMeshProUGUI textComp = placeholder.AddComponent<TextMeshProUGUI>();
            textComp.text = text;
            textComp.fontSize = 14;
            textComp.color = new Color32(148, 163, 184, 255);
            textComp.alignment = TextAlignmentOptions.Left;
            textComp.verticalAlignment = VerticalAlignmentOptions.Middle;
            if (m_defaultFont != null) textComp.font = m_defaultFont;

            return textComp;
        }

        private static TextMeshProUGUI CreateTextComponent(Transform parent)
        {
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(parent);

            RectTransform rect = textObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.pivot = new Vector2(0, 0.5f);
            rect.offsetMin = new Vector2(16, 0);
            rect.offsetMax = new Vector2(-16, 0);

            TextMeshProUGUI textComp = textObj.AddComponent<TextMeshProUGUI>();
            textComp.fontSize = 14;
            textComp.color = new Color32(30, 41, 59, 255);
            textComp.alignment = TextAlignmentOptions.Left;
            textComp.verticalAlignment = VerticalAlignmentOptions.Middle;
            if (m_defaultFont != null) textComp.font = m_defaultFont;

            return textComp;
        }

        private static ColorBlock GetInputFieldColorBlock()
        {
            ColorBlock colors = new ColorBlock();
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            colors.disabledColor = new Color(0.7f, 0.7f, 0.7f);
            colors.colorMultiplier = 1;
            return colors;
        }

        private static GameObject CreateToggle(string name, string label, Transform parent)
        {
            GameObject toggleObj = new GameObject(name);
            toggleObj.transform.SetParent(parent);

            RectTransform rect = toggleObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(160, 24);

            Toggle toggle = toggleObj.AddComponent<Toggle>();

            GameObject labelObj = new GameObject("Label");
            labelObj.transform.SetParent(toggleObj.transform);

            RectTransform labelRect = labelObj.AddComponent<RectTransform>();
            labelRect.anchorMin = new Vector2(0, 0.5f);
            labelRect.anchorMax = new Vector2(0, 0.5f);
            labelRect.pivot = new Vector2(0.5f, 0.5f);
            labelRect.localPosition = new Vector3(89, 0, 0);
            labelRect.sizeDelta = new Vector2(132, 16);

            TextMeshProUGUI labelText = labelObj.AddComponent<TextMeshProUGUI>();
            labelText.text = label;
            labelText.fontSize = 14;
            labelText.color = new Color32(100, 116, 139, 255);
            labelText.alignment = TextAlignmentOptions.Left;
            labelText.verticalAlignment = VerticalAlignmentOptions.Middle;
            if (m_defaultFont != null) labelText.font = m_defaultFont;

            GameObject background = new GameObject("Background");
            background.transform.SetParent(toggleObj.transform);

            RectTransform bgRect = background.AddComponent<RectTransform>();
            bgRect.anchorMin = new Vector2(0, 0.5f);
            bgRect.anchorMax = new Vector2(0, 0.5f);
            bgRect.pivot = new Vector2(0.5f, 0.5f);
            bgRect.localPosition = new Vector3(10, 0, 0);
            bgRect.sizeDelta = new Vector2(20, 20);

            Image bgImage = background.AddComponent<Image>();
            bgImage.color = new Color32(248, 250, 252, 255);

            Image borderImage = new GameObject("Border").AddComponent<Image>();
            borderImage.transform.SetParent(background.transform);
            borderImage.rectTransform.anchorMin = Vector2.zero;
            borderImage.rectTransform.anchorMax = Vector2.one;
            borderImage.rectTransform.offsetMin = Vector2.zero;
            borderImage.rectTransform.offsetMax = Vector2.zero;
            borderImage.color = new Color32(203, 213, 225, 255);

            GameObject checkmark = new GameObject("Checkmark");
            checkmark.transform.SetParent(background.transform);

            RectTransform checkRect = checkmark.AddComponent<RectTransform>();
            checkRect.anchorMin = Vector2.zero;
            checkRect.anchorMax = Vector2.one;
            checkRect.offsetMin = new Vector2(2, 2);
            checkRect.offsetMax = new Vector2(-2, -2);

            Image checkImage = checkmark.AddComponent<Image>();
            checkImage.color = Color.white;

            toggle.targetGraphic = bgImage;
            toggle.graphic = checkImage;
            toggle.isOn = false;

            return toggleObj;
        }

        private static GameObject CreateButton(string name, string text, Transform parent)
        {
            GameObject buttonObj = new GameObject(name);
            buttonObj.transform.SetParent(parent);

            RectTransform rect = buttonObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(320, 50);

            Image image = buttonObj.AddComponent<Image>();
            image.color = new Color32(37, 99, 235, 255);

            Button button = buttonObj.AddComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            button.colors = GetButtonColorBlock();

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(buttonObj.transform);

            RectTransform textRect = textObj.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            TextMeshProUGUI textComp = textObj.AddComponent<TextMeshProUGUI>();
            textComp.text = text;
            textComp.fontSize = 16;
            textComp.fontStyle = FontStyles.Bold;
            textComp.color = Color.white;
            textComp.alignment = TextAlignmentOptions.Center;
            if (m_defaultFont != null) textComp.font = m_defaultFont;

            return buttonObj;
        }

        private static ColorBlock GetButtonColorBlock()
        {
            ColorBlock colors = new ColorBlock();
            colors.normalColor = new Color32(37, 99, 235, 255);
            colors.highlightedColor = new Color32(29, 78, 216, 255);
            colors.pressedColor = new Color32(30, 64, 175, 255);
            colors.selectedColor = new Color32(37, 99, 235, 255);
            colors.disabledColor = new Color32(148, 163, 184, 255);
            colors.colorMultiplier = 1;
            return colors;
        }
    }
}