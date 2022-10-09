using PilotBrothersSafe.Common;
using PilotBrothersSafe.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using ToggleState = PilotBrothersSafe.Models.ToggleState;

namespace PilotBrothersSafe.Controls
{
    public class ToggleField : Grid
    {
        private const int DefaultCellSize = 20;

        private readonly ToggleFieldLogic _toggleFieldLogic = new ToggleFieldLogic();

        private Toggle[,] _toggles;

        private ToggleState[] ToggleStates = { ToggleState.Horizontal, ToggleState.Vertical };

        private Random _random = new Random();

        public static readonly DependencyProperty DataProviderProperty = DependencyProperty.Register(
                nameof(DataProvider),
                typeof(IDataProvider),
                typeof(ToggleField),
                new PropertyMetadata(
                    (sourceObject, args) =>
                    {
                        var mineField = sourceObject as ToggleField;
                        mineField?.CellDataProviderChanged(
                            (IDataProvider)args.OldValue,
                            (IDataProvider)args.NewValue);
                    }));

        public IDataProvider DataProvider
        {
            get
            {
                return (IDataProvider)GetValue(DataProviderProperty);
            }
            set
            {
                SetValue(DataProviderProperty, value);
            }
        }

        private void CellDataProviderChanged(IDataProvider oldProvider, IDataProvider newProvider)
        {
            RemoveUnactualField(oldProvider);
            _toggleFieldLogic.DataProvider = newProvider;
            DrawNewField();
            _toggleFieldLogic.RandomMixStates();
            _toggleFieldLogic.RandomMixStates();
        }

        private void RemoveUnactualField(IDataProvider oldData)
        {
            if (oldData == null)
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
            var provider = DataProvider;
            _toggles = new Toggle[provider.LenghtOnOneSide, provider.LenghtOnOneSide];
            var state = ToggleStates[_random.Next(ToggleStates.Length)];

            for (int i = 0; i < provider.LenghtOnOneSide; i++)
            {
                var definition = new RowDefinition() { Height = new GridLength(DefaultCellSize * 2) };
                RowDefinitions.Add(definition);
            }

            for (int j = 0; j < provider.LenghtOnOneSide; j++)
            {
                var definition = new ColumnDefinition() { Width = new GridLength(DefaultCellSize * 2) };
                ColumnDefinitions.Add(definition);
            }

            for (int i = 0; i < provider.LenghtOnOneSide; i++)
            {
                for (int j = 0; j < provider.LenghtOnOneSide; j++)
                {
                    var toggle = new Toggle(state) { Width = DefaultCellSize, Height = DefaultCellSize };
                    Children.Add(toggle);
                    SetColumn(toggle, j);
                    SetRow(toggle, i);

                    _toggles[i, j] = toggle;
                }
            }
            provider.ResetCounter(state);
            _toggleFieldLogic.AttachToggles(_toggles);

        }
    }
}
