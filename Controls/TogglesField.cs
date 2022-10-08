using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using PilotBrothersSafe.Models;
using System.Windows.Automation;
using ToggleState = PilotBrothersSafe.Models.ToggleState;
using PilotBrothersSafe.Common;

namespace PilotBrothersSafe.Controls
{
    public class ToggleField : Grid
    {
        private const int DefaultCellSize = 20;

        private readonly ToggleFieldLogic _toggleFieldLogic = new ToggleFieldLogic();

        private Toggle[,] _toggles;

        private ToggleState[] ToggleStates = { ToggleState.Horizontal, ToggleState.Vertical };

        private Random _random = new Random();

        public static readonly DependencyProperty LenghtOnOneSideProperty;


        static ToggleField()
        {
            LenghtOnOneSideProperty = DependencyProperty.Register(
                nameof(LenghtOnOneSide),
                typeof(int),
                typeof(ToggleField),
                new PropertyMetadata(
                    (sourceObject, args) =>
                    {
                        var mineField = sourceObject as ToggleField;
                        mineField?.CellDataProviderChanged(
                            (int)args.OldValue,
                            (int)args.NewValue);
                    }));
        }

        public ToggleField()
        {

        }



        public int LenghtOnOneSide
        {
            get
            {
                return (int)GetValue(LenghtOnOneSideProperty);
            }
            set
            {
                SetValue(LenghtOnOneSideProperty, value);
            }
        }

        private void CellDataProviderChanged(int oldProvider, int newProvider)
        {
            RemoveUnactualField(oldProvider);
            _toggleFieldLogic.LenghtOnOneSide = newProvider;
            DrawNewField();
            _toggleFieldLogic.RandomMixStates();
            _toggleFieldLogic.RandomMixStates();
        }

        private void RemoveUnactualField(int oldData)
        {
            if (oldData == 0)
            {
                return;
            }

            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

            _toggles.ForEach(RemoveLogicalChild);
            _toggles.ForEach(
                (toggle =>
                {
                    RemoveLogicalChild(toggle);
                    RemoveVisualChild(toggle);
                    toggle.InputBindings.Clear();
                    toggle.Content = null;
                }));
            _toggles = null;
        }

        private void DrawNewField()
        {
            _toggles = new Toggle[LenghtOnOneSide, LenghtOnOneSide];
            var state = ToggleStates[_random.Next(ToggleStates.Length)];

            for (int i = 0; i < LenghtOnOneSide; i++)
            {
                var definition = new RowDefinition() { Height = new GridLength(DefaultCellSize * 2) };
                RowDefinitions.Add(definition);
            }

            for (int j = 0; j < LenghtOnOneSide; j++)
            {
                var definition = new ColumnDefinition() { Width = new GridLength(DefaultCellSize * 2) };
                ColumnDefinitions.Add(definition);
            }

            for (int i = 0; i < LenghtOnOneSide; i++)
            {
                for (int j = 0; j < LenghtOnOneSide; j++)
                {
                    var toggle = new Toggle(state) { Width = DefaultCellSize, Height = DefaultCellSize };
                    Children.Add(toggle);
                    SetColumn(toggle, j);
                    SetRow(toggle, i);

                    _toggles[i, j] = toggle;
                }
            }

            _toggleFieldLogic.AttachToggles(_toggles);
            
        }
    }
}
