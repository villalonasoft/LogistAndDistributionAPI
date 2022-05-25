using CEDIS.Core.Pgsql.Persistences;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Linq;

namespace CEDIS.Core.Pgsql.Frameworks.Helpers
{
    public static class IdentityIncrement<T> where T : class
    {
        private const int INITIAL_VALUE = 0;

        public static int GetNextId(ApplicationDbContext DbContext, string columnName, string propertyValue, string propertyToIncrease, int increment)
        {
            try
            {
                dynamic entity = DbContext.Set<T>().ToList()
                    .Where(e => e.GetType().GetProperty(columnName).GetValue(e, null).ToString() == propertyValue)
                    .OrderBy(a => a.GetType().GetProperty(propertyToIncrease).GetValue(a, null))
                    .LastOrDefault();

                if (entity == null)
                {
                    return INITIAL_VALUE + increment;
                }
                else
                {
                    return GetPropertyValue(propertyToIncrease, entity) + increment;
                }
            }
            catch (RuntimeBinderException)
            {
                throw new RuntimeBinderException($"La propiedad Id no existe en la clase {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static int GetPropertyValue(string propertyToIncrease, dynamic entity)
        {
            try
            {
                var property = entity.GetType().GetProperty(propertyToIncrease);
                var currentValue = property.GetValue(entity).ToString();

                var valueResult = int.Parse(currentValue);
                return valueResult;
            }
            catch (InvalidCastException exception)
            {
                throw new InvalidCastException($"Ocurrió un error al convertir el número dentro de la columna a incrementar. {exception.Message}");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
