using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProcessoSeletivo.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {

        private static List<Item> Itens;
        private HashSet<Item> Car = new HashSet<Item>();

        public ShopController()
        {
            GetInstance();
        }

        public static IList GetInstance()
        {
            if (Itens == null)
                Itens = new List<Item>();

            return Itens;
        }


        /// <summary>
        /// Retorna todos os itens da lista
        /// </summary>
        [HttpGet]
        [Route("All")]
        public string GetAll()
        {
            var itens = "";
            var total = 0.0m;

            foreach (var i in Itens)
            { 
                itens += i.Nome + " - Quantidade: " + i.Quantidade + " - R$" + i.Quantidade * i.ValorUnitario + "\n" ;
                //subtotal = i.Quantidade * i.ValorUnitario;
                total += i.Quantidade * i.ValorUnitario;
            }


            return String.Format("[{0}] produto(s) no carrinho:\n\n{1} \n\nTotal = R${2}", Itens.Count, itens, total);
        }


        /// <summary>
        /// Retorna itens pela posicao
        /// </summary>
        [HttpGet]
        [Route("Product/{id}")]
        public string GetProductById(string id)
        {
            var val = Convert.ToInt32(id);
            return Itens[val].ToString();
        }

         /// <summary>
         /// Adiciona novos itens a lista
         /// </summary>
        [HttpPost]
        [Route("Product/New/{product}/{quant}/{price}")]
        public void InsertItens(string product, string quant, string price)
        {
            Item i = new Item()
            {
                Quantidade = Convert.ToInt32(quant),
                Nome = product,
                ValorUnitario = Convert.ToDecimal(price)
            };

            Itens.Add(i);
        }


        /// <summary>
        /// Remove itens da lista
        /// </summary>
        [HttpDelete]
        [Route("Product/Remove/{id}")]
        public void RemoveItens(string id)
        {
            var val = Convert.ToInt32(id);
            Itens.RemoveAt(val);
        }


        /// <summary>
        /// Atualiza itens da lista
        /// </summary>
        [HttpPut]
        [Route("Product/Update/{product}/{quant}/{price}")]
        //[Authorize]
        public void UpdateItens(string product, string quant, string price)
        {
            var cont = 0;

            foreach (var i in Itens)
            {
                if (i.Nome.Equals(product)) {
                    this.InsertItens(product, quant, price);
                    this.RemoveItens(cont.ToString());

                    break;
                }

                cont++;   
            }
        }


        /// <summary>
        /// Limpa a lista
        /// </summary>
        [Route("Product/Clear")]
        public void ClearItens()
        {
            Itens.Clear();
        }
    }
}
