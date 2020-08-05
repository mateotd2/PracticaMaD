using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventDao
{
    public class SportEventDaoEntityFramework :
        GenericDaoEntityFramework<SportEvent, Int64>, ISportEventDao
    {
        public List<SportEvent> FindByKeywords(string keyword, int startIndex, int count, long category = 0)
        {
            //DbSet<SportEvent> sportEvents = Context.Set<SportEvent>();

            #region Usig Entity SQL query and Object Service.

            //String query = "SELECT VALUE a FROM PracticaMaDEntitiesContainer.SportEvent AS a";
            /////Inicializo los parametros.
            //List<ObjectParameter> listaParametros = new List<ObjectParameter>();
            /////Comprobar si existe Category
            //int j = 0;
            //if (category != null)
            //{
            //    query += " LOWER(ev_category) LIKE LOWER(@category)";
            //    ObjectParameter param = new ObjectParameter("category", category);
            //    listaParametros.Add(param);
            //    j = 1;
            //}

            /////Recorrer los keywords para
            //if (keywords != null && wordCount > 0)
            //{
            //    query += " WHERE";
            //    for (int i=j ; i < wordCount; i++)
            //    {
            //        if (j==1)
            //        {
            //            query += " AND";
            //        }
            //        query += " LOWER(ev_name) LIKE LOWER(@word)";
            //        ObjectParameter paramaux = new ObjectParameter("word", strlist[i]);
            //        //Añadir parametros uno a uno y luego meterlos en una lista
            //        listaParametros.Add(paramaux);

            // }

            // ///DEBERIA UTILIZAR UN ARRAY EN VEZ DE UNA LISTA? List<SportEvent> result =
            // Context.Database.SqlQuery<SportEvent>(query, listaParametros).Skip(startIndex). Take(count).ToList();

            //}
            //return new List<SportEvent>();

            #endregion Usig Entity SQL query and Object Service.

            #region Using Linq

            DbSet<SportEvent> sportEvents = Context.Set<SportEvent>();

            if (category == 0)
            {
                List<SportEvent> result =
                    (from element in sportEvents
                     where element.ev_name.ToLower().Contains(keyword.ToLower()) //TODO: falta el filtro por categoria.(añadirlas a sql)
                     orderby element.ev_date
                     select element).Skip(startIndex).Take(count).ToList();

                return result;
            }
            else
            {
                List<SportEvent> result =
                    (from element in sportEvents
                     where element.ev_name.ToLower().Contains(keyword.ToLower()) //TODO: falta el filtro por categoria.(añadirlas a sql)
                     where element.ev_category.Equals(category)
                     orderby element.ev_date
                     select element).Skip(startIndex).Take(count).ToList();

                return result;
            }

            #endregion Using Linq
        }
    }
}