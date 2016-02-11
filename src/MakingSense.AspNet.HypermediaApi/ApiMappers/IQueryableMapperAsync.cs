using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.ApiMappers
{
	public interface IQueryableMapperAsync<TIn, TOut>
	{
		Task<IEnumerable<TOut>> MapAsync(IQueryable<TIn> queriable);
	}
}
