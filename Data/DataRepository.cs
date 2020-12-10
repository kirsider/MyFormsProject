using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyForms.Data;
using MyForms.Data.Models;

namespace MyForms.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:MySqlConnection"];
        }

        public Form GetForm(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var form = connection.QueryFirstOrDefault<Form>(
                    @"SELECT id, name, description FROM form
                      WHERE id=@FormId", new {FormId = formId}
                );

                form.Questions = GetFormQuestions(formId);
                form.UserId = GetFormUserId(formId);
                return form;
            }
        }

        public IEnumerable<Question> GetFormQuestions(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var questions = connection.Query<Question>(
                    @"SELECT question.id, question.name, question.type, question.options 
                      FROM form JOIN form_question ON(form_question.fid = form.id)
                      JOIN question ON(form_question.qid = question.id)
                      WHERE form.id=@FormId", new { FormId = formId }
                );

                return questions;
            }
        }

        public string GetFormUserId(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string uid = connection.QueryFirstOrDefault<string>(
                    @"SELECT uid FROM user_form
                      WHERE fid=@FormId", new { FormId = formId }
                );

                return uid;
            }
        }

        public IEnumerable<Result> GetFormResults(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var results = connection.Query<Result>(
                    @"SELECT id, answers FROM result
                      WHERE fid=@FormId", new { FormId = formId }
                );

                return results;
            }
        }
    }
}
