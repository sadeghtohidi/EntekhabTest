using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Entekhab.Domain.Entities;
using Entekhab.Services.Contract;
using Entekhab.Services.Dto.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IRepository<SalaryPersonal> _salaryPersonalRepository;
        public SalaryService(IRepository<SalaryPersonal> salaryPersonalRepository)
        {
            _salaryPersonalRepository = salaryPersonalRepository;
        }

        public async Task Add_UpdateAsync(AddUpdateSalaryRequestDto requestDto)
        {
            var salary = await _salaryPersonalRepository.TableNoTracking.FirstOrDefaultAsync(f => f.EmployeeNumber == requestDto.EmployeeNumber && f.YearMonth == requestDto.YearMonth);
            if (salary == null)  // Insert
            {
                await _salaryPersonalRepository.AddAsync(new SalaryPersonal()
                {
                    YearMonth = requestDto.YearMonth,
                    BasicSalary = requestDto.BasicSalary,
                    EmployeeNumber = requestDto.EmployeeNumber,
                    Allowance = requestDto.Allowance,
                    Tax = requestDto.Tax,
                    Transportation = requestDto.Transportation,
                    OverTimeAmount = OverTimeCalculator(requestDto.BasicSalary + requestDto.Allowance) ,
                    TotalSalary = requestDto.BasicSalary + requestDto.Allowance + requestDto.Transportation + OverTimeCalculator(requestDto.BasicSalary + requestDto.Allowance) - requestDto.Tax,
                });
                return;
            }
            else   // Update
            {
                salary.YearMonth = requestDto.YearMonth;
                salary.BasicSalary = requestDto.BasicSalary;
                salary.EmployeeNumber = requestDto.EmployeeNumber;
                salary.Allowance = requestDto.Allowance;
                salary.Tax = requestDto.Tax;
                salary.Transportation = requestDto.Transportation;
                salary.OverTimeAmount = OverTimeCalculator(requestDto.BasicSalary + requestDto.Allowance);
                salary.TotalSalary = requestDto.BasicSalary + requestDto.Allowance + requestDto.Transportation + OverTimeCalculator(requestDto.BasicSalary + requestDto.Allowance) - requestDto.Tax;
                

                await _salaryPersonalRepository.UpdateAsync(salary);
            }
        }

        public async Task DeleteAsync(DeleteSalaryRequestDto requestDto)
        {
            var salary =await _salaryPersonalRepository.Table.FirstOrDefaultAsync(f => f.YearMonth == requestDto.YearMonth && f.EmployeeNumber == requestDto.EmployeeNumber);
            if (salary != null)
                await _salaryPersonalRepository.DeleteAsync(salary);
        }
        private decimal OverTimeCalculator(decimal amount)
        {
            // Todo : Calculate OverTime

            return 0;
        }

    }
}
