﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.ApiMappers
{
	public interface IApiMapper<TIn, TOut>
	{
		void Fill(TIn input, TOut output);
	}
}
