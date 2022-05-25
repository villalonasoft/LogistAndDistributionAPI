using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload
{
    public static class OleDbCommandExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public static void SkipValidateNullOnSave(this OleDbCommand command)
        {
            /**
             * Intelifarma database ins't well configurated. Any value can't be null. We can allow null setting this command below.
             * http://vfphelp.com/vfp9/_59k0sp3t9.htm
             */
            using (var newCommand = command.Connection.CreateCommand())
            {
                newCommand.CommandType = CommandType.Text;
                newCommand.CommandText = "SET NULL OFF;";
                newCommand.ExecuteNonQuery();
            }
        }
    }
}
