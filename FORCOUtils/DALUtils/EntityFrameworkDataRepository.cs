using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FORCOUtils.ExceptionUtils;

namespace FORCOUtils.DALUtils
{
    public class EntityFrameworkDataRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext fContext{ get; set; }
        private bool fDisposeContextOnEveryAction { get; set; }

        private EntityFrameworkDataRepository()
        {
            
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Builds up an instance of this repository
        /// </summary>
        /// <param name="aContext">The DbContext instance holding your entities</param>
        /// <param name="aDisposeContextOnEveryAction">If true the passed context will be disposed after every action. If false you'll need to dispose this context using the Dispose method</param>
        public EntityFrameworkDataRepository(DbContext aContext, bool aDisposeContextOnEveryAction = true)
        {
            fDisposeContextOnEveryAction = aDisposeContextOnEveryAction;
            fContext = aContext;
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <returns>IList of objects with all the objects of the specified type on the repository</returns>
        public IList<T> GetAll()
        {
            try
            {
                List<T> _List;

                fContext.Configuration.LazyLoadingEnabled = false;

                IQueryable<T> _DbQuery = fContext.Set<T>();

                _List = _DbQuery.AsNoTracking().ToList();

                fContext.Configuration.LazyLoadingEnabled = true;

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        ///<author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects of specified type from the repository source
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type</returns>
        public IList<TU> GetAllWithSelect<TU>(Func<T, TU> aSelectFunc) where TU : class
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified type from the repository source in a specific order
        ///  </summary>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of objects with all the objects of the specified type on the repository</returns>
        public IList<T> GetAll(Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order</returns>
        public IList<TU> GetAllWithSelect<TU>(Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc, OrderFunctionType aOrderFunctionType) where TU : class
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified  type from the repository source in a specific order and using pagination
        /// </summary>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of objects with all the objects of the specified type on the repository</returns>
        public IList<T> GetAll(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order and using pagination
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order and using pagination</returns>
        public IList<TU> GetAllWithSelect<TU>(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }


        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with all the objects of the specified type on the repository. Lazy loading used</returns>
        public IList<T> GetAllLazyLoading(params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;

                IQueryable<T> _DbQuery = fContext.Set<T>();

                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = _DbQuery.AsNoTracking().ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }


        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects fomr the source and selects from them some specific fields
        ///  </summary>
        ///  <typeparam name="TU">The type for the selected object to return</typeparam>
        ///  <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type. Lazy loading used</returns>
        public IList<TU> GetAllLazyLoadingWithSelect<TU>(Func<T, TU> aSelectFunc, params Expression<Func<T, object>>[] aNavigationProperties) where TU : class
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();

                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = _DbQuery.AsNoTracking().Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified type from the repository source in a specific order
        ///  </summary>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with all the objects of the specified type on the repository. Lazy loading used</returns>
        public IList<T> GetAllLazyLoading(Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;
                IQueryable<T> _DbQuery = fContext.Set<T>();

                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().OrderByDescending(aOrderFunc).ToList() : _DbQuery.AsNoTracking().OrderBy(aOrderFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aOrderFunc"></param>
        /// <param name="aOrderFunctionType"></param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order. Lazy loading used</returns>
        public IList<TU> GetAllLazyLoadingWithSelect<TU>(Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc, OrderFunctionType aOrderFunctionType,
            params Expression<Func<T, object>>[] aNavigationProperties) where TU : class
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();

                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().OrderByDescending(aOrderFunc).Select(aSelectFunc).ToList() : _DbQuery.AsNoTracking().OrderBy(aOrderFunc).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified  type from the repository source in a specific order and using pagination
        /// </summary>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with all the objects of the specified type on the repository. Lazy loading used</returns>
        public IList<T> GetAllLazyLoading(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType,
            params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);


                aPage -= 1;
                var _AllItemsCount = _DbQuery.Count();
                if (_AllItemsCount % aCount == 0)
                    aPageCount = _AllItemsCount / aCount;
                else
                    aPageCount = _AllItemsCount / aCount + 1;

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().OrderByDescending(aOrderFunc).Skip(aPage * aPageCount).Take(aPageCount).ToList() : _DbQuery.AsNoTracking().OrderBy(aOrderFunc).Skip(aPage * aPageCount).Take(aPageCount).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
                
                
                return _List;
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order and using pagination
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order and using pagination</returns>
        public IList<TU> GetAllLazyLoadingWithSelect<TU>(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);


                aPage -= 1;
                var _AllItemsCount = _DbQuery.Count();
                if (_AllItemsCount % aCount == 0)
                    aPageCount = _AllItemsCount / aCount;
                else
                    aPageCount = _AllItemsCount / aCount + 1;

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().OrderByDescending(aOrderFunc).Skip(aPage * aPageCount).Take(aPageCount).Select(aSelectFunc).ToList() : _DbQuery.AsNoTracking().OrderBy(aOrderFunc).Skip(aPage * aPageCount).Take(aPageCount).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
                
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression</returns>
        public IList<T> GetList(Func<T, bool> aWhere)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression</returns>
        public IList<TU> GetListWithSelect<TU>(Func<T, bool> aWhere, Func<T, TU> aSelectFunc) where TU : class
        {
            throw new NotImplementedException();
        }


        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified type from the repository source in a specific order
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression</returns>
        public IList<T> GetList(Func<T, bool> aWhere, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <returns>IList of new objects created from the list of objects in the repository according to boolean expression and converted to the select function out type in a specific order</returns>
        public IList<TU> GetListWithSelect<TU>(Func<T, bool> aWhere, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc, OrderFunctionType aOrderFunctionType) where TU : class
        {
            try
            {
                List<TU> _List;
                
                fContext.Configuration.LazyLoadingEnabled = false;
                IQueryable<T> _DbQuery = fContext.Set<T>();
                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().OrderByDescending(aOrderFunc).Select(aSelectFunc).ToList() : _DbQuery.AsNoTracking().OrderBy(aOrderFunc).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }


        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified  type from the repository source in a specific order and using pagination
        /// </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression with specific order and using pagination</returns>
        public IList<T> GetList(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order and using pagination
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order and using pagination</returns>
        public IList<TU> GetListWithSelect<TU>(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }


        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression. Lazy loading used</returns>
        public IList<T> GetListLazyLoading(Func<T, bool> aWhere, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;
                
            IQueryable<T> _DbQuery = fContext.Set<T>();
            foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                _DbQuery = _DbQuery.Include(_NavigationProperty);

            _List = _DbQuery.AsNoTracking().Where(aWhere).ToList();

            if (fDisposeContextOnEveryAction)
            {
                fContext.Dispose();

            }            

            return _List;
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        ///  <summary>
        ///  Fetch objects of specified type from the repository source
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression. Lazy loading used</returns>
        public IList<TU> GetListWithSelectLazyLoading<TU>(Func<T, bool> aWhere, Func<T, TU> aSelectFunc, params Expression<Func<T, object>>[] aNavigationProperties) where TU : class
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = _DbQuery.AsNoTracking().Where(aWhere).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified type from the repository source in a specific order
        ///  </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression. Lazy loading used</returns>
        public IList<T> GetListLazyLoading(Func<T, bool> aWhere, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().Where(aWhere).OrderByDescending(aOrderFunc).ToList() : _DbQuery.AsNoTracking().Where(aWhere).OrderBy(aOrderFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of new objects created from the list of objects in the repository according to boolean expression and converted to the select function out type in a specific order. Lazy loading used</returns>
        public IList<TU> GetListWithSelectLazyLoading<TU>(Func<T, bool> aWhere, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc, OrderFunctionType aOrderFunctionType,
            params Expression<Func<T, object>>[] aNavigationProperties) where TU : class
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().Where(aWhere).OrderByDescending(aOrderFunc).Select(aSelectFunc).ToList() : _DbQuery.AsNoTracking().Where(aWhere).OrderBy(aOrderFunc).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _List;
                
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        ///  Fetch objects of specified  type from the repository source in a specific order and using pagination
        /// </summary>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of objects with objects of the specified type on the repository according to boolean expression with specific order and using pagination. Lazy loading used</returns>
        public IList<T> GetListLazyLoading(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc,
            OrderFunctionType aOrderFunctionType, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<T> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);


                aPage -= 1;
                var _AllItemsCount = _DbQuery.Count(aWhere);
                if (_AllItemsCount % aCount == 0)
                    aPageCount = _AllItemsCount / aCount;
                else
                    aPageCount = _AllItemsCount / aCount + 1;

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().Where(aWhere).OrderByDescending(aOrderFunc).Skip(aPage * aCount).Take(aCount).ToList() : _DbQuery.AsNoTracking().Where(aWhere).OrderBy(aOrderFunc).Skip(aPage * aCount).Take(aCount).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
                
                return _List;
                
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch objects from the source and selects from them some specific fields in a specific order and using pagination
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the list to return</param>
        /// <param name="aPage">Sets the page to load</param>
        /// <param name="aCount">Sets the number of items per page to load</param>
        /// <param name="aPageCount">Returns the number of pages after fetchig the results</param>
        /// <param name="aOrderFunc">Sets the  order by funtion for the collection</param>
        /// <param name="aSelectFunc">A select function to return a new type only with specified fields</param>
        /// <param name="aOrderFunctionType">Sets the ordering type for the collection</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>IList of new objects created from the list of all objects in the repository and converted to the select function out type in a specific order and using pagination. Lazy loading used</returns>
        public IList<TU> GetListWithSelectLazyLoading<TU>(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                List<TU> _List;
                
                IQueryable<T> _DbQuery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                    _DbQuery = _DbQuery.Include(_NavigationProperty);


                aPage -= 1;
                var _AllItemsCount = _DbQuery.Count(aWhere);
                if (_AllItemsCount % aCount == 0)
                    aPageCount = _AllItemsCount / aCount;
                else
                    aPageCount = _AllItemsCount / aCount + 1;

                _List = aOrderFunctionType == OrderFunctionType.Descending ? _DbQuery.AsNoTracking().Where(aWhere).OrderByDescending(aOrderFunc).Skip(aPage * aCount).Take(aCount).Select(aSelectFunc).ToList() : _DbQuery.AsNoTracking().Where(aWhere).OrderBy(aOrderFunc).Skip(aPage * aCount).Take(aCount).Select(aSelectFunc).ToList();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
                
                return _List;
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <returns>The frist object in the source or null if none exist</returns>
        public T GetSingle()
        {
            try
            {
                T _Single = null;

                fContext.Configuration.LazyLoadingEnabled = false;

                IQueryable<T> _Dbquery = fContext.Set<T>();

                _Single = _Dbquery.AsNoTracking().FirstOrDefault();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }



                return _Single;

            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <param name="aWhere">Boolean expression to filter the object to return</param>
        /// <returns>A single object with all associated objects to it</returns>
        public T GetSingle(Func<T, bool> aWhere)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the object to return</param>
        /// <returns>A single object according to select function out type</returns>
        public TU GetSingle<TU>(Func<T, bool> aWhere) where TU : class
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>A single object with all associated objects to it</returns>
        public T GetSingleLazyLoading(params Expression<Func<T, object>>[] aNavigationProperties)
        {
            throw new NotImplementedException();
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <param name="aWhere">Boolean expression to filter the object to return</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>A single object with all associated objects to it. Lazy Loading used</returns>
        public T GetSingleLazyLoading(Func<T, bool> aWhere, params Expression<Func<T, object>>[] aNavigationProperties)
        {
            try
            {
                T _Single = null;
                
                IQueryable<T> _Dbquery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                {
                    _Dbquery = _Dbquery.Include(_NavigationProperty);
                }

                _Single = _Dbquery.AsNoTracking().FirstOrDefault(aWhere);

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }


                return _Single;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Fetch a single object from the source
        /// </summary>
        /// <typeparam name="TU">The type for the selected object to return</typeparam>
        /// <param name="aWhere">Boolean expression to filter the object to return</param>
        /// <param name="aNavigationProperties">The properties to include on the lazy loading</param>
        /// <returns>A single object according to select function out type. Lazy Loading used</returns>
        public TU GetSingleLazyLoading<TU>(Func<T, bool> aWhere, params Expression<Func<T, object>>[] aNavigationProperties) where TU : class
        {
            try
            {
                TU _Single = null;
                
                IQueryable<T> _Dbquery = fContext.Set<T>();
                foreach (Expression<Func<T, object>> _NavigationProperty in aNavigationProperties)
                {
                    _Dbquery = _Dbquery.Include(_NavigationProperty);
                }

                _Single = Convert.ChangeType(_Dbquery.AsNoTracking().FirstOrDefault(aWhere), typeof(TU)) as TU;


                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _Single;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        public int Count()
        {
            try
            {
                IQueryable<T> _Dbquery = fContext.Set<T>();

                var _Count = _Dbquery.Count();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _Count;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
            
        }

        public int Count(Func<T, bool> aWhere)
        {

            try
            {
                IQueryable<T> _Dbquery = fContext.Set<T>();
                var _Count = _Dbquery.Count(aWhere);

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }

                return _Count;
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }

            
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Adds new items to the source
        /// </summary>
        /// <param name="aItems">The items to add</param>
        public void Add(params T[] aItems)
        {
            try
            {
                
                    foreach (T _Item in aItems)
                    {
                        fContext.Entry(_Item).State = EntityState.Added;
                    }
                    fContext.SaveChanges();

                    if (fDisposeContextOnEveryAction)
                    {
                        fContext.Dispose();

                    }
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Updates items on the source
        /// </summary>
        /// <param name="aItems">The items to update</param>
        public void Update(params T[] aItems)
        {
            try
            {
                
                foreach (T _Item in aItems)
                {
                    fContext.Entry(_Item).State = EntityState.Modified;
                }
                fContext.SaveChanges();

                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Removes items from the source
        /// </summary>
        /// <param name="aItems"></param>
        public void Remove(params T[] aItems)
        {
            try
            {
                
                foreach (T _Item in aItems)
                {
                    fContext.Entry(_Item).State = EntityState.Deleted;
                }
                fContext.SaveChanges();


                if (fDisposeContextOnEveryAction)
                {
                    fContext.Dispose();

                }
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        /// <author>Jose Alexis Hernandez-jahernandezricardo@gmail.com</author>
        /// <summary>
        /// Removes all items from the source
        /// </summary>
        public void RemoveAll()
        {
            try
            {
                
                    fContext.DeleteAllEntities<T>();
                    if (fDisposeContextOnEveryAction)
                    {
                        fContext.Dispose();

                    }
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);
            }
        }

        public void DisposeContext()
        {
            fContext.Dispose();
        }


        private Exception HandleException(Exception aException, MethodBase aMethodInfo)
        {
            return new  Exception("Error on DAL method " + aMethodInfo + ". Line:" + aException.LineNumber() + ". Entity type: " + typeof(T), aException);
        }


    }
}
