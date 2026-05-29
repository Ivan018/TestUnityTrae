using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TestUnityTrae.UI
{
    public class LoginController : MonoBehaviour
    {
        [Header("UI组件")]
        [SerializeField] private TMP_InputField input_account;
        [SerializeField] private TMP_InputField input_password;
        [SerializeField] private Toggle toggle_remember;
        [SerializeField] private Button btn_login;

        private void Awake()
        {
            if (btn_login != null)
            {
                btn_login.onClick.AddListener(OnLoginClicked);
            }
            LoadRememberedCredentials();
        }

        private void OnDestroy()
        {
            if (btn_login != null)
            {
                btn_login.onClick.RemoveListener(OnLoginClicked);
            }
        }

        public void SetReferences(TMP_InputField account, TMP_InputField password, Toggle remember, Button login)
        {
            input_account = account;
            input_password = password;
            toggle_remember = remember;
            btn_login = login;
            
            if (btn_login != null)
            {
                btn_login.onClick.AddListener(OnLoginClicked);
            }
        }

        private void OnLoginClicked()
        {
            string account = input_account.text.Trim();
            string password = input_password.text;

            if (string.IsNullOrEmpty(account))
            {
                ShowMessage("请输入账号");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("请输入密码");
                return;
            }

            if (toggle_remember.isOn)
            {
                SaveCredentials(account, password);
            }
            else
            {
                ClearCredentials();
            }

            Debug.Log($"登录请求: 账号={account}, 密码=******");
            ShowMessage("登录成功！");
        }

        private void SaveCredentials(string account, string password)
        {
            PlayerPrefs.SetString("Login_Account", account);
            PlayerPrefs.SetString("Login_Password", password);
            PlayerPrefs.SetInt("Login_Remember", 1);
            PlayerPrefs.Save();
        }

        private void LoadRememberedCredentials()
        {
            if (toggle_remember != null && PlayerPrefs.GetInt("Login_Remember", 0) == 1)
            {
                if (input_account != null)
                {
                    input_account.text = PlayerPrefs.GetString("Login_Account", string.Empty);
                }
                if (input_password != null)
                {
                    input_password.text = PlayerPrefs.GetString("Login_Password", string.Empty);
                }
                toggle_remember.isOn = true;
            }
        }

        private void ClearCredentials()
        {
            PlayerPrefs.DeleteKey("Login_Account");
            PlayerPrefs.DeleteKey("Login_Password");
            PlayerPrefs.SetInt("Login_Remember", 0);
            PlayerPrefs.Save();
        }

        private void ShowMessage(string message)
        {
            Debug.Log(message);
        }
    }
}