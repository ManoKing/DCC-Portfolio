using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uMVVM.Sources.Infrastructure
{
    public class ViewModelBase
    {
        private bool _isInitialized;//是否初始化完成
        public ViewModelBase ParentViewModel { get; set; }
        public bool IsRevealed { get; private set; }
        public bool IsRevealInProgress { get; private set; }
        public bool IsHideInProgress { get; private set ; }

        //初始化  只在UI第一次显示时调用
        protected virtual void OnInitialize()
        {
            
        }

        //在该UI被激活时调用
        public virtual void OnStartReveal()
        {
            IsRevealInProgress = true;
            //在开始显示的时候进行初始化操作
            if (!_isInitialized)
            {
                OnInitialize();
                _isInitialized = true;
            }
        }

        //在UI打开动画播放完毕调用
        public virtual void OnFinishReveal()
        {
            IsRevealInProgress = false;
            IsRevealed = true;
        }

        //在UI隐藏时调用
        public virtual void OnStartHide()
        {
            IsHideInProgress = true;

        }

        //在UI隐藏动画播放完毕之后调用
        public virtual void OnFinishHide()
        {
            IsHideInProgress = false;
            IsRevealed = false;
        }

        public virtual void OnDestory()
        {
            
        }

    }
}
