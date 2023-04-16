using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    // Bu class veri nesnesi ile response dödürür.
    // Bu class'ın amacı, response döndürürken, veri nesnesi dışında başka bilgiler de döndürmek.
    // Örneğin, response'un status kodu, hata mesajları, vs. 
   
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Success(T data, int statusCode)
        {
            return new CustomResponseDto<T> {Data = data, StatusCode = statusCode, Errors = null};
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> {StatusCode = statusCode};
        }

        public static CustomResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new CustomResponseDto<T> { Errors = errors, StatusCode = statusCode};
        }

        public static CustomResponseDto<T> Fail(string errors, int statusCode)
        {
            return new CustomResponseDto<T> { Errors = new List<string> {errors}, StatusCode = statusCode };
        }
    }
}
