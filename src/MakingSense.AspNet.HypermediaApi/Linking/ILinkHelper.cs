﻿using MakingSense.AspNet.Abstractions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.Linking
{
	public interface ILinkHelper
	{
		Maybe<Link> ToAction<T>(Expression<Func<T, Task>> expression) where T : Controller;
		Maybe<Link> ToAction<T>(Expression<Action<T>> expression) where T : Controller;
		Maybe<Link> ToSelf(object values = null);
		Maybe<Link> ToHomeAccount();
	}
}
