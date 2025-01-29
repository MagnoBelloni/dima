using Dima.Api.Data;
using Dima.Core.BaseResponses;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<BaseResponse<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Amount = request.Amount,
                    Type = request.Type,
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction, 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BaseResponse<Transaction?>(null, 500, "Não foi possivel criar sua transação");
            }
        }

        public async Task<BaseResponse<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (transaction is null)
                {
                    return new BaseResponse<Transaction?>(null, 404, "Transação não encontrada");
                }

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BaseResponse<Transaction?>(null, 500, "Não foi possivel deletar sua transação");
            }
        }

        public async Task<BaseResponse<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new BaseResponse<Transaction?>(null, 404, "Nenhuma transação foi encontrada")
                    : new BaseResponse<Transaction?>(transaction);
            }
            catch (Exception)
            {
                return new BaseResponse<Transaction?>(null, 500, "Não foi possivel obter sua transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                try
                {
                    request.StartDate ??= DateTime.Now.GetFirstDay();
                    request.EndDate ??= DateTime.Now.GetLastDay();
                }
                catch (Exception)
                {
                    return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel converter a data informada");
                }

                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt <= request.EndDate && x.UserId == request.UserId)
                    .OrderBy(x => x.CreatedAt);

                var transactions = await query
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel listar suas transações");
            }
        }

        public async Task<BaseResponse<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (transaction is null)
                {
                    return new BaseResponse<Transaction?>(null, 404, "Transação não encontrada");
                }

                transaction.Title = request.Title;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Amount = request.Amount;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);

                await context.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BaseResponse<Transaction?>(null, 500, "Não foi possivel atualizar sua transação");
            }
        }
    }
}
