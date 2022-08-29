using Application.Exceptions;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.ADN.Commands
{
    public class Save
    {
        public class Command : IRequest<bool>
        {
            public string[] dna { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.dna).NotNull().WithMessage("ADN is required")
                    .Must(z => z != null && z.Length >= 4).WithMessage("ADN must have a total of more than 4")
                    .ForEach(y => y.NotEmpty().WithMessage("ADN cannot be empty"));
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly DBContext context;

            public Handler(DBContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var data = BuildSequencesVertical(request);
                bool isMutant = ValidateMutant(data);
                await SaveData(string.Join(",", request.dna), isMutant, cancellationToken);

                return isMutant;
            }

            private List<string> BuildSequencesVertical(Command request)
            {
                var sequences = request.dna.ToList();
                var sequencesLength = sequences.Count;
                var data = request.dna;
                var allowedLetters = new string[] { "A", "C", "G", "T" };
                string sequence = string.Empty;

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Length != sequencesLength)
                        throw new ValidateException("ADN array must be NxN");

                    sequence = data[0][i].ToString();
                    for (int j = 1; j < data.Length; j++)
                    {
                        var letter = data[j][i].ToString();
                        if (!allowedLetters.Contains(letter))
                            throw new ValidateException("ADN contains letter not allowed");

                        sequence += letter;
                    }
                    
                    sequences.Add(sequence);
                }

                for (int i = 1 - data.Length; i < data.Length; i++)
                {
                    sequence = String.Empty;
                    for (int x = -Math.Min(0, i), y = Math.Max(0, i); x < data.Length && y < data.Length; x++, y++)
                    {
                        sequence += data[x][y];
                    }

                    if (sequence.Length >= 4)
                        sequences.Add(sequence);
                }

                return sequences;
            }

            private bool ValidateMutant(List<string> sequences)
            {
                int countSequence = 0;
                int count = 1;

                foreach (var sequence in sequences)
                {
                    for (int i = 0; i < sequence.Length; i++)
                    {
                        var letter = sequence[i].ToString();
                        for (int j = i + 1; j < sequence.Length; j++)
                        {
                            if (sequence[j].ToString() == letter)
                                count++;
                            else
                                break;
                        }

                        if (count >= 4)
                            countSequence++;

                        count = 1;
                    }
                }

                return countSequence > 1;
            }

            private async Task SaveData(string adn, bool isMutant, CancellationToken cancellationToken)
            {
                var adnEntity = new Domain.ADN
                {
                    ANDRegister = adn,
                    IsMutant = isMutant
                };

                context.ADN.Add(adnEntity);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
