using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ADN.Queries
{
    public class List
    {
        public class Query: IRequest<StatsDto> { }

        public class Handler: IRequestHandler<Query, StatsDto>
        {
            private readonly DBContext context;

            public Handler(DBContext context)
            {
                this.context = context;
            }

            public async Task<StatsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await context.ADN.ToListAsync(cancellationToken);
                if(data != null && data.Any())
                {
                    double countHuman = data.Count(x => !x.IsMutant);
                    double countMutant = data.Count() - countHuman;
                    var stats = new StatsDto
                    {
                        CountHumanDna = Convert.ToInt32(countHuman),
                        CountMutantDna = Convert.ToInt32(countMutant),
                        Ratio = (countHuman == 0 ? countMutant :  countMutant / countHuman)
                    };
                    return stats;

                }
                return new StatsDto();
            }
        }
    }
}
