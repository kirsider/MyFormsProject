using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyForms.Data.Models;

namespace MyForms.Data
{
    public interface IDataRepository
    {
        Form GetForm(int formId);
        IEnumerable<Question> GetFormQuestions(int formId);
        IEnumerable<Result> GetFormResults(int formId);
        string GetFormUserId(int formId);

        Form PostForm(FormPostRequest formPostRequest);
        Question PostQuestion(QuestionPostRequest questionPostRequest);

    }
}
