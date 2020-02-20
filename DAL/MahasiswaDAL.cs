using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WorkshopASP.Models;
using System;

namespace WorkshopASP.DAL
{
    public class MahasiswaDAL : IMahasiswa
    {
        private IConfiguration _config;
        public MahasiswaDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(string id)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"delete from Mahasiswa where 
                                  Nim=@Nim";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@Nim",id);
                try{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx}");
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Mahasiswa> GetAll()
        {
            List<Mahasiswa> lstMhs = new List<Mahasiswa>();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa 
                                  order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        lstMhs.Add(new Mahasiswa{
                            Nim = dr["Nim"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            IPK = Convert.ToDouble(dr["IPK"]),
                            Email = dr["Email"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstMhs;
            }
        }

        public Mahasiswa GetById(string id)
        {
            Mahasiswa mhs = new Mahasiswa();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa 
                                  where Nim=@Nim";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@Nim",id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        mhs.Nim = dr["Nim"].ToString();
                        mhs.Nama = dr["Nama"].ToString();
                        mhs.IPK = Convert.ToDouble(dr["IPK"]);
                        mhs.Email = dr["Email"].ToString();
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return mhs;
        }

        public void Insert(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"insert into Mahasiswa(Nim,Nama,IPK,Email) 
                                values(@Nim,@Nama,@IPK,@Email)";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@Nim",mhs.Nim);
                cmd.Parameters.AddWithValue("@Nama",mhs.Nama);
                cmd.Parameters.AddWithValue("@IPK",mhs.IPK);
                cmd.Parameters.AddWithValue("@Email",mhs.Email);
                try{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }catch(SqlException sqlEx){
                    throw new Exception(sqlEx.Message);
                }
                catch(Exception ex){
                    throw new Exception(ex.Message);
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"update Mahasiswa set Nama=@Nama,
                IPK=@IPK,Email=@Email where Nim=@Nim";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@Nama",mhs.Nama);
                cmd.Parameters.AddWithValue("@IPK",mhs.IPK);
                cmd.Parameters.AddWithValue("@Email",mhs.Email);
                cmd.Parameters.AddWithValue("@Nim",mhs.Nim);
                try{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }catch(SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}