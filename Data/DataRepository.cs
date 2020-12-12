using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyForms.Data;
using MyForms.Data.Models;
using System.Text.Json.Serialization;
using System.Text.Json;


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
                    @"SELECT id, name AS fname, description FROM form
                      WHERE id=@FormId;", new {FormId = formId}
                );

                form.Questions = GetFormQuestions(formId);
                form.Uid = GetFormUserId(formId);
                return form;
            }
        }

        public IEnumerable<Form> GetUserForms(string userId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var forms = connection.Query<Form>(
                    @"SELECT form.id, user_form.uid as uid, form.name AS fname, form.description FROM form 
                      JOIN user_form ON (form.id = user_form.fid)
                      WHERE user_form.uid=@UserId;", new { UserId = userId }
                );

                return forms;
            }
        }

        public Question[] GetFormQuestions(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var questions = connection.Query<Question>(
                    @"SELECT question.id, question.name AS qname, question.type, question.options 
                      FROM form JOIN form_question ON(form_question.fid = form.id)
                      JOIN question ON(form_question.qid = question.id)
                      WHERE form.id=@FormId;", new { FormId = formId }
                );

                foreach (var question in questions)
                {
                    question.Options = JsonSerializer.Deserialize<string[]>(question.Options);
                }
                return questions.ToArray();
            }
        }

        public string GetFormUserId(int formId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string uid = connection.QueryFirstOrDefault<string>(
                    @"SELECT uid FROM user_form
                      WHERE fid=@FormId;", new { FormId = formId }
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
                    @"SELECT id, fid, answers FROM result 
                      WHERE fid=@FormId;", new { FormId = formId }
                );

                return results;
            }
        }

        public Form PostForm(FormPostRequest formPostRequest)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"INSERT INTO form(name, description) VALUES (@Name, @Description);",
                    new
                    {
                        formPostRequest.Name,
                        formPostRequest.Description
                    }
                );

                var form = connection.QueryFirstOrDefault<Form>(
                    @"SELECT id, name, description FROM form ORDER BY id DESC LIMIT 1;"
                );

                connection.Execute(
                    @"INSERT INTO user_form(uid, fid) VALUES(@Uid, @Fid);",
                    new
                    {
                        Uid = formPostRequest.UserId,
                        Fid = form.Id
                    }
                );

                foreach (var questionPostRequest in formPostRequest.Questions)
                {
                    var question = PostQuestion(questionPostRequest);

                    connection.Execute(
                        @"INSERT INTO form_question(fid, qid) VALUES(@Fid, @Qid);",
                        new
                        {
                            Fid = form.Id,
                            Qid = question.Id
                        }
                    );
                }

                return form;
            }
        }

        public Question PostQuestion(QuestionPostRequest questionPostRequest)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"INSERT INTO question(name, type, options) VALUES (@QName, @Type, @Options);",
                    new { 
                        questionPostRequest.QName, 
                        questionPostRequest.Type, 
                        Options = JsonSerializer.Serialize(questionPostRequest.Options)
                    }
                );

                return connection.QueryFirstOrDefault<Question>(
                    @"SELECT id, name, type, options FROM question ORDER BY id DESC LIMIT 1;"
                );
            }
        }

        public Result PostResult(ResultPostRequest resultPostRequest)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"INSERT INTO result(fid, answers) VALUES (@Fid, @Answers);",
                    new
                    {
                        resultPostRequest.Fid,
                        resultPostRequest.Answers
                    }
                );

                return connection.QueryFirstOrDefault<Result>(
                    @"SELECT id, fid, answers FROM result ORDER BY id DESC LIMIT 1;"
                );
            }
        }
    }
}
