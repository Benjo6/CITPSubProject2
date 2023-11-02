using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        
        public string? MovieId { get; set; }
        public string? PersonId { get; set; }
        public int? Ordering { get; set; }
        public string? Category { get; set; }
        public string? Job { get; set; }
        public string? Characters { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual Person? Person { get; set; }
    }
    
    public class CreateRoleDTO
    {
        
        public string? MovieId { get; set; }
        public string? PersonId { get; set; }
        public int? Ordering { get; set; }
        public string? Category { get; set; }
        public string? Job { get; set; }
        public string? Characters { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual Person? Person { get; set; }
    }
    
    public class UpdateRoleDTO
    {
        public int Id { get; set; }
        
        public string? MovieId { get; set; }
        public string? PersonId { get; set; }
        public int? Ordering { get; set; }
        public string? Category { get; set; }
        public string? Job { get; set; }
        public string? Characters { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual Person? Person { get; set; }
    }
}

