using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.Dto;
using PimIVBackend.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Validator;
using static PimIVBackend.Models.Folio;

namespace PimIVBackend.Business
{
    public class FolioBusiness : IFolioServices
    {
        private readonly AppDbContext _context;

        public FolioBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClosedFolioDto> DoCheckOut(int folioId)
        {
            Guard.Validate(validator =>
                            validator
                                .NotDefault(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                );

            var folio = await _context.Folios
                                        .Include(x => x.FolioItems)
                                        .Include(x => x.Reservation)
                                        .Include(x => x.Reservation).ThenInclude(x => x.MainGuest)
                                        .FirstOrDefaultAsync(x => x.Id == folioId);

            if (folio != null && folio.FolioItems != null && folio.Reservation != null && folio.Reservation.MainGuest != null)
            {
                if (folio.FolioStatus == FolioStatusEnum.Closed)
                    throw new Exception("Impossível fechar uma conta que já está fechada");
                else
                    return folio.CheckOut();
            }
            else
                throw new Exception("Internal Server Error");

        }

        public async Task PostProduct(int folioId, int productId, int guestId, decimal quantity)
        {
            Guard.Validate(validator =>
                            validator
                                .NotDefault(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                .NotDefault(productId, nameof(productId), $"{nameof(productId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(productId, nameof(productId), $"{nameof(productId)} está com um valor inválido")
                                .NotDefault(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido")
                                .NotDefault(productId, nameof(quantity), $"{nameof(quantity)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(quantity, nameof(quantity), $"{nameof(quantity)} está com um valor inválido")
                                );

            var folio = await _context.Folios
                                         .Include(x => x.FolioItems)
                                         .Include(x => x.Entities)
                                         .FirstOrDefaultAsync(x => x.Id == folioId);
            var guest = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == guestId);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var folioItem = default(FolioItem);

            if(!folio.Entities.Any(x => x.EntityId == guest.Id))
            {
                throw new Exception("Impossível lançar um item para um hóspede que não consta nessa conta");
            }

            if (folio != null &&
               folio.FolioItems != null &&
               folio.FolioStatus == FolioStatusEnum.Opened)
            {
                if (guest != null)
                {
                    if (product != null)
                    {
                        folioItem = new FolioItem(product, guest, quantity);
                    }

                }
            } else
            {
                throw new Exception("Operação cancelada por dados inconsistentes");
            }

            folio.PostProduct(folioItem);
        }

        public async Task RemoveProduct(int folioId, int folioItemId)
        {
            Guard.Validate(validator =>
                            validator
                                .NotDefault(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(folioId, nameof(folioId), $"{nameof(folioId)} está com um valor inválido")
                                .NotDefault(folioItemId, nameof(folioItemId), $"{nameof(folioItemId)} está com um valor inválido")
                                .IsGratterThanZeroAndPositive(folioItemId, nameof(folioItemId), $"{nameof(folioItemId)} está com um valor inválido")
                                );

            var folio = await _context.Folios
                                        .Include(x => x.FolioItems)
                                        .FirstOrDefaultAsync(x => x.Id == folioId);

            var folioItem = await _context.FolioItems.FirstOrDefaultAsync(x => x.Id == folioItemId);

            if(folio != null && folioItem != null && folio.FolioItems != null)
            {
                if(folio.FolioItems.Count == 0)
                {
                    throw new Exception("Essa conta não possui items para serem removidos");
                }

                if(!folio.FolioItems.Any(x => x.Id == folioItem.Id))
                {
                    throw new Exception("O item informado não existe nessa conta");
                }

                folio.RemoveProduct(folioItem);
            } else
            {
                throw new Exception("Formulário inconsistente");
            }
        }
    }
}
