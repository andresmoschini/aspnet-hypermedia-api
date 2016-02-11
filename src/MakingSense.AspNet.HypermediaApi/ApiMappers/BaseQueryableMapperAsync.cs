using MakingSense.AspNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.ApiMappers
{
	/// <summary>
	/// It helps to create a IQueryable friendly async mapper.
	/// </summary>
	/// <remarks>
	/// TProjection cannot be the same type than TOut
	/// </remarks>
	public abstract class BaseQueryableMapperAsync1<TIn, TOut, TProjection> : IApiMapperAsync<TIn, TOut>, IQueryableMapperAsync<TIn, TOut>
		where TOut : new()
	{
		public async Task FillAsync(TIn input, TOut output) =>
			await FillAsync((await MapToProjectionAsync(Enumerable.Repeat(input, 1).AsQueryable())).First(), output);

		public virtual async Task<IEnumerable<TOut>> MapAsync(IQueryable<TIn> queriable) =>
			await Task.WhenAll(
				(await MapToProjectionAsync(queriable)).Select(async x =>
				{
					var output = new TOut();
					await FillAsync(x, output);
					return output;
				}).ToArray());

		protected abstract Task FillAsync(TProjection input, TOut output);

		/// <summary>
		/// Maps queryable results to an enumerable of an intermediate representation (projection)
		/// </summary>
		/// <remarks>
		/// Take into account that queryable results could contains null values
		/// </remarks>
		protected abstract Task<IEnumerable<TProjection>> MapToProjectionAsync(IQueryable<TIn> queriable);
	}

	public abstract class BaseQueryableMapperAsync2<TIn, TOut, TProjection> : IApiMapper<TIn, TOut>, IQueryableMapperAsync<TIn, TOut>
		where TOut : new()
	{
		public void Fill(TIn input, TOut output) =>
			Fill((MapToProjectionAsync(Enumerable.Repeat(input, 1).AsQueryable()).WaitAndGetValue()).First(), output);

		public virtual async Task<IEnumerable<TOut>> MapAsync(IQueryable<TIn> queriable) =>
				(await MapToProjectionAsync(queriable)).Select(x =>
				{
					var output = new TOut();
					Fill(x, output);
					return output;
				});

		protected abstract void Fill(TProjection input, TOut output);

		/// <summary>
		/// Maps queryable results to an enumerable of an intermediate representation (projection)
		/// </summary>
		/// <remarks>
		/// Take into account that queryable results could contains null values
		/// </remarks>
		protected abstract Task<IEnumerable<TProjection>> MapToProjectionAsync(IQueryable<TIn> queriable);
	}
}
