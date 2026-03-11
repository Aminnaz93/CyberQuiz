using CyberQuiz.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CyberQuiz.DAL.Data
{
    public static class UserResultSeeder
    {
        public static async Task SeedTestUserResultsAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("UserResultSeeder");

            // Hämta testanvändaren
            var testUser = await userManager.FindByEmailAsync("user@test.com");
            if (testUser == null)
            {
                logger.LogWarning("Test user 'user@test.com' not found. Run UserSeeder first.");
                return;
            }

            // Kolla om användaren redan har resultat
            var existingResults = await context.UserResults
                .Where(ur => ur.UserId == testUser.Id)
                .CountAsync();

            if (existingResults > 0)
            {
                logger.LogInformation("Test user already has {Count} quiz results.", existingResults);
                return;
            }

            logger.LogInformation("Seeding quiz results for user '{UserName}'...", testUser.UserName);

            var userResults = new List<UserResult>();
            var answeredAt = DateTime.UtcNow.AddDays(-7);

            // SubCategory 1: Grundläggande Nätverk (80% rätt - 8 av 10 rätt)
            // Options 1,4,7,10,13,16,19,22 = rätt (första per fråga), 26,29 = fel
            // Guid.NewGuid() = unikt ID för denna seed-omgång
            userResults.AddRange(CreateResults(testUser.Id, 1, new[] { 1, 4, 7, 10, 13, 16, 19, 22, 26, 29 }, Guid.NewGuid(), ref answeredAt));

            // SubCategory 4: OWASP Top 10 (40% rätt - 4 av 10 rätt)
            // Options 91,94,97,100 = rätt, 104,107,110,113,116,119 = fel
            // Eget Guid – separar omgång från subkategori 1
            userResults.AddRange(CreateResults(testUser.Id, 4, new[] { 91, 94, 97, 100, 104, 107, 110, 113, 116, 119 }, Guid.NewGuid(), ref answeredAt));

            // SubCategory 7: Phishing (60% rätt - 6 av 10 rätt)
            // Options 181,184,187,190,193,196 = rätt, 200,203,206,209 = fel
            // Eget Guid – separar omgång från ovanstående
            userResults.AddRange(CreateResults(testUser.Id, 7, new[] { 181, 184, 187, 190, 193, 196, 200, 203, 206, 209 }, Guid.NewGuid(), ref answeredAt));

            // Spara till databasen
            context.UserResults.AddRange(userResults);
            await context.SaveChangesAsync();

            var correctCount = userResults.Count(ur => ur.IsCorrect);
            var totalCount = userResults.Count;
            var successRate = (double)correctCount / totalCount * 100;

            logger.LogInformation("✅ Seeded {Total} quiz results ({Correct} correct, {successRate:F1}%)", 
                totalCount, correctCount, successRate);
        }

        private static List<UserResult> CreateResults(
            string userId,
            int subCategoryId,
            int[] answerOptionIds,
            Guid attemptId,
            ref DateTime answeredAt)
        {
            var results = new List<UserResult>();

            foreach (var answerOptionId in answerOptionIds)
            {
                results.Add(new UserResult
                {
                    UserId = userId,
                    QuestionId = GetQuestionIdFromAnswerOption(answerOptionId),
                    AnswerOptionId = answerOptionId,
                    IsCorrect = IsAnswerCorrect(answerOptionId),
                    AnsweredAt = answeredAt,
                    AttemptId = attemptId
                });

                answeredAt = answeredAt.AddMinutes(2);
            }

            return results;
        }

        private static int GetQuestionIdFromAnswerOption(int answerOptionId)
        {
            return (answerOptionId - 1) / 3 + 1;
        }

        private static bool IsAnswerCorrect(int answerOptionId)
        {
            return (answerOptionId - 1) % 3 == 0;
        }
    }
}
