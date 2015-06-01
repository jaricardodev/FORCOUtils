using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

       
        /// <summary>
        /// Builds up a instance of this repository
        /// </summary>
        /// <param name="aContext">A EF DbContext context holding all the entities type</param>
        /// <param name="aDisposeContextOnEveryAction">If true after every action the changes will be saved to the source and the context will be inmediatly disposed. If false the action will not be saved to the source until SaveChanges() method is called and the context will be disposed using the Dispose() method. The Dispose() method will automatically call SaveChanges() before disposing the context. </param>
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

        
        public IList<TU> GetAllWithSelect<TU>(Func<T, TU> aSelectFunc) where TU : class
        {
            throw new NotImplementedException();
        }

      
        public IList<T> GetAll(Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

      
        public IList<TU> GetAllWithSelect<TU>(Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc, OrderFunctionType aOrderFunctionType) where TU : class
        {
            throw new NotImplementedException();
        }

      
        public IList<T> GetAll(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

        
        public IList<TU> GetAllWithSelect<TU>(int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }


       
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

        
        public IList<T> GetList(Func<T, bool> aWhere)
        {
            throw new NotImplementedException();
        }

        
        public IList<TU> GetListWithSelect<TU>(Func<T, bool> aWhere, Func<T, TU> aSelectFunc) where TU : class
        {
            throw new NotImplementedException();
        }


       
        public IList<T> GetList(Func<T, bool> aWhere, Func<T, object> aOrderFunc, OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

       
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


        
        public IList<T> GetList(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }

       
        public IList<TU> GetListWithSelect<TU>(Func<T, bool> aWhere, int aPage, int aCount, out int aPageCount, Func<T, object> aOrderFunc, Func<T, TU> aSelectFunc,
            OrderFunctionType aOrderFunctionType)
        {
            throw new NotImplementedException();
        }


      
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

      
        public T GetSingle(Func<T, bool> aWhere)
        {
            throw new NotImplementedException();
        }

        
        public TU GetSingle<TU>(Func<T, bool> aWhere) where TU : class
        {
            throw new NotImplementedException();
        }

      
        public T GetSingleLazyLoading(params Expression<Func<T, object>>[] aNavigationProperties)
        {
            throw new NotImplementedException();
        }

       
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

        /// <summary>
        /// Returns the count for all the entities of the repository type
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Returns the count for all the entities of the repository type that satisfies the condition
        /// </summary>
        /// <param name="aWhere">Condition to do the count</param>
        /// <returns></returns>
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

        public void Add(params T[] aItems)
        {
            try
            {
                
                    foreach (T _Item in aItems)
                    {
                        fContext.Entry(_Item).State = EntityState.Added;
                    }
                    

                    if (fDisposeContextOnEveryAction)
                    {
                        fContext.SaveChanges();
                        fContext.Dispose();

                    }
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

     
        public void Update(params T[] aItems)
        {
            try
            {
                
                foreach (T _Item in aItems)
                {
                    fContext.Entry(_Item).State = EntityState.Modified;
                }
                if (fDisposeContextOnEveryAction)
                {
                    fContext.SaveChanges();
                    fContext.Dispose();

                }
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

        public void Remove(params T[] aItems)
        {
            try
            {
                
                foreach (T _Item in aItems)
                {
                    fContext.Entry(_Item).State = EntityState.Deleted;
                }
               


                if (fDisposeContextOnEveryAction)
                {
                    fContext.SaveChanges();
                    fContext.Dispose();

                }
               
            }
            catch (Exception _Exception)
            {
                MethodBase _MetodInfo = MethodInfo.GetCurrentMethod();
                throw HandleException(_Exception, _MetodInfo);

            }
        }

       
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

        /// <summary>
        /// Disposes the context passed to this repository if its not disposed automatically
        /// </summary>
        public void DisposeContext()
        {
            fContext.SaveChanges();
            fContext.Dispose();
        }


        private Exception HandleException(Exception aException, MethodBase aMethodInfo)
        {
            return new  Exception("Error on DAL method " + aMethodInfo + ". Line:" + aException.LineNumber() + ". Entity type: " + typeof(T), aException);
        }


        public void SaveChanges()
        {
            fContext.SaveChanges();
        }


    }
}
