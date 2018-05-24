
using uMVVM.Sources.Infrastructure;
using uMVVM.Sources.Models;
using uMVVM.Sources.ViewModels;
using UnityEngine.UI;

namespace uMVVM.Sources.Views
{
    public class SetupView:UnityGuiView<SetupViewModel>
    {

        public InputField nameInputField;//名字输入框
        public Text nameMessageText;

        public InputField jobInputField;//职业输入框
        public Text jobMessageText;

        public InputField atkInputField;//攻击力输入框
        public Text atkMessageText;

        public Slider successRateSlider;
        public Text successRateMessageText;

        public Toggle joinToggle;//是否加入作战组toggle
        public Button joinInButton;//加入Btn
        public Button waitButton;//暂不加入Btn
        public SetupViewModel ViewModel { get { return (SetupViewModel)BindingContext; } }//当前ViewModel

        //初始化
        protected override void OnInitialize()
        {
            base.OnInitialize();
            //添加属性名，及值改变回调事件
            Binder.Add<string>("Name", OnNamePropertyValueChanged);
            Binder.Add<string>("Job",OnJobPropertyValueChanged);
            Binder.Add<int>("ATK",OnATKPropertyValueChanged);
            Binder.Add<float>("SuccessRate",OnSuccessRatePropertyValueChanged);
            Binder.Add<State>("State",OnStatePropertyValueChanged);

        }

        //监听成功率改变事件
        private void OnSuccessRatePropertyValueChanged(float oldValue, float newValue)
        {
            successRateMessageText.text = newValue.ToString("F2");//保留两位小数
        }

        private void OnATKPropertyValueChanged(int oldValue, int newValue)
        {
            atkMessageText.text = newValue.ToString();
        }

        private void OnJobPropertyValueChanged(string oldValue, string newValue)
        {
            jobMessageText.text = newValue.ToString();
        }

        private void OnNamePropertyValueChanged(string oldValue, string newValue)
        {
            nameMessageText.text = newValue.ToString();
        }

        //状态切换
        private void OnStatePropertyValueChanged(State oldValue, State newValue)
        {
            switch (newValue)
            {
                case State.JoinIn:
                    joinInButton.interactable = true;
                    waitButton.interactable = false;
                    break;
                case State.Wait:
                    joinInButton.interactable = false;
                    waitButton.interactable = true;
                    break;
            }
        }

        public void iptName_ValueChanged()
        {
            ViewModel.Name.Value = nameInputField.text;
        }

        public void iptJob_ValueChanged()
        {
            ViewModel.Job.Value = jobInputField.text;
        }

        public void iptATK_ValueChanged()
        {
            int result;
            if (int.TryParse(atkInputField.text,out result))
            {
                ViewModel.ATK.Value = int.Parse(atkInputField.text);
            }
        }

        public void sliderSuccessRate_ValueChanged()
        {
            ViewModel.SuccessRate.Value = successRateSlider.value;
        }

        public void toggle_ValueChanged()
        {
            if (joinToggle.isOn)
            {
                ViewModel.State.Value = State.JoinIn;
            }
            else
            {
                ViewModel.State.Value = State.Wait;
            }
        }

        //加入按钮点击事件
        public void JoinInBattleTeam()
        {
            ViewModel.JoininCurrentTeam();
        }

        //暂不加入按钮点击事件
        public void JoinInClan()
        {
            ViewModel.JoininClan();
        }
    }
}
