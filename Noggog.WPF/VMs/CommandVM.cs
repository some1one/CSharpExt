using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Noggog.WPF
{
    public class CommandVM : ViewModel
    {
        private readonly ObservableAsPropertyHelper<bool> _CanExecute;
        public bool CanExecute => _CanExecute.Value;

        public ReactiveCommand<Unit, Unit> Command { get; private set; }

        private CommandVM(ReactiveCommand<Unit, Unit> cmd)
        {
            Command = cmd;
            _CanExecute = cmd.CanExecute
                .ToProperty(this, nameof(CanExecute), initialValue: false);
        }

        public static CommandVM Factory(ReactiveCommand<Unit, Unit> cmd)
        {
            return new CommandVM(cmd);
        }
    }
}
