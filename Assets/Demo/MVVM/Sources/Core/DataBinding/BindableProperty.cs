using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace uMVVM.Sources.Infrastructure
{

    //绑定属性 T表示绑定属性的数据类型（用于在ViewModel中声明存储UI数据的变量）
    public class BindableProperty<T>
    {
        //关于值改变的委托
        public delegate void ValueChangedHandler(T oldValue, T newValue);

        //改变事件
        public ValueChangedHandler OnValueChanged;

        //可绑定属性的值
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!Equals(_value, value))
                {
                    T old = _value;
                    _value = value;
                    ValueChanged(old, _value);
                }
            }
        }

        private void ValueChanged(T oldValue, T newValue)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(oldValue, newValue);
            }
        }

        public override string ToString()
        {
            return (Value != null ? Value.ToString() : "null");
        }
    }
}
