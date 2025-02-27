﻿using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ClientApplication.Common.Abstract
{
    public class IClientCommand<T> : IRequest<T>
    {
        public readonly IDialogService? DialogService;
        public IClientCommand(IDialogService _dialogService)
        {
            DialogService = _dialogService;
        }

        public IClientCommand()
        {
             
        }
    }
}
