using Clio.Demo.Abstraction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clio.Demo.Abstractions.Interface
{
	public interface IEntityCache<T> where T : class, IEntity
	{
		T              Get    (int key);
		IEnumerable<T> Get    ();
		void           Put(T entity);
		void           Hydrate(IEnumerable<T> entities);
		void           Flush  ();
	
		TimeSpan Duration { get; set; }
 	}
}
