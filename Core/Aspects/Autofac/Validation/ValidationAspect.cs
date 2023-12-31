﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect --> metot başı sonu çalışacak yapı
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            //defensive coding 
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderömeye çalıştığın validatotype atanaibliyor mu? kullanıcı her şeyi yazabilir
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Reflection --> çalışma anında bir şeyleri çalıştırabilmemizi(newleymemizi) sağlıyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //gelen validatorun base type'ını bul, onun generic argümanlarından ilkini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // ilgili metodun parametrelerine bak, validation tool'u kullanarak.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
