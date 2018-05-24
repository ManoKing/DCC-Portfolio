using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using uMVVM.Sources.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace Assets.Sources.Core.DataBinding
{
    public class PropertyBinder<T> where T:ViewModelBase
    {
        private delegate void BindHandler(T viewmodel);//属性绑定委托
        private delegate void UnBindHandler(T viewmodel);//取消属性绑定委托

        private readonly List<BindHandler> _binders=new List<BindHandler>();//绑定事件集合
        private readonly List<UnBindHandler> _unbinders=new List<UnBindHandler>();//取消绑定事件集合
        
        //添加当前属性的属性改变事件
        public void Add<TProperty>(string name,BindableProperty<TProperty>.ValueChangedHandler valueChangedHandler )
        {
            //获取ViewModel中的字段  字段类型是BindableProperty<T>类型
            var fieldInfo = typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Public);
            if (fieldInfo == null)
            {
                throw new Exception(string.Format("Unable to find bindableproperty field '{0}.{1}'", typeof(T).Name, name));
            }
            //添加事件
            _binders.Add(viewmodel =>
            {
                GetPropertyValue<TProperty>(name, viewmodel, fieldInfo).OnValueChanged += valueChangedHandler;
            });

            _unbinders.Add(viewModel =>
            {
                GetPropertyValue<TProperty>(name, viewModel, fieldInfo).OnValueChanged -= valueChangedHandler;
            });

        }

        private  BindableProperty<TProperty> GetPropertyValue<TProperty>(string name, T viewModel,FieldInfo fieldInfo)
        {
            var value = fieldInfo.GetValue(viewModel);//获取BindableProperty
            BindableProperty<TProperty> bindableProperty = value as BindableProperty<TProperty>;
            if (bindableProperty == null)
            {
                throw new Exception(string.Format("Illegal bindableproperty field '{0}.{1}' ", typeof(T).Name, name));
            }

            return bindableProperty;
        }

        //绑定
        public void Bind(T viewmodel)
        {
            if (viewmodel!=null)
            {
                for (int i = 0; i < _binders.Count; i++)
                {
                    _binders[i](viewmodel);
                }
            }
        }

        public void Unbind(T viewmodel)
        {
            if (viewmodel!=null)
            {
                for (int i = 0; i < _unbinders.Count; i++)
                {
                    _unbinders[i](viewmodel);
                }
            }
        }

    }
}
