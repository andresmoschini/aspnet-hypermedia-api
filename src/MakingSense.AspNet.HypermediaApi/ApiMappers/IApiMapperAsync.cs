using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.ApiMappers
{
	public interface IApiMapperAsync<TIn, TOut>
	{
		Task FillAsync(TIn input, TOut output);
	}
}
