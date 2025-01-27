using System.Globalization;
using System.Text;

namespace aoc24.Problems;

using Circuit = Dictionary<Day24.Wire, int>;

// https://adventofcode.com/2024/day/24
public sealed class Day24 : IProblem<long>
{
    public long Solve(string input) => CombineWireValuesOptimized(input);

    private readonly Circuit circuit = [];

    private readonly Queue<Operation> operationQueue = [];

    private long CombineWireValuesOptimized(ReadOnlySpan<char> input)
    {
        circuit.Clear();
        operationQueue.Clear();

        var iterator = input.Split(InputReader.NewLine);

        // Read the current wire values
        while (iterator.MoveNext())
        {
            var line = input[iterator.Current];

            if (line.IsEmpty)
                break;

            if (line.IndexOf(": ") is int sepInx)
                circuit.Add(Wire.Parse(line[..sepInx]), int.Parse(line[(sepInx + 2)..]));
        }

        // Read and enqueue all operations
        while (iterator.MoveNext())
        {
            var line = input[iterator.Current];
            operationQueue.Enqueue(Operation.Parse(line));
        }

        while (operationQueue.TryDequeue(out var operation))
        {
            // If the operation can't be executed (because one of the wires is still not signaling)
            // move the operation back to the end of the queue.
            if (!operation.TryExecuteOn(circuit))
                operationQueue.Enqueue(operation);
        }

        // Generate and parse the binary number out of the wire values
        Span<char> binary = stackalloc char[64];
        int current = 0;

        var wires = circuit.Keys.ToArray();
        Array.Sort(wires);

        for (int i = wires.Length - 1; i >= 0; i--)
        {
            var wire = wires[i];
            if (wire.IsTarget)
                binary[current++] = circuit[wire] == 0 ? '0' : '1';
        }

        long @decimal = long.Parse(binary, NumberStyles.BinaryNumber);
        return @decimal;
    }

    public long CombineWireValues(string input)
    {
        if (input.Split(InputReader.NewLine + InputReader.NewLine) is not [var wiresString, var opsString])
            return 0;

        circuit.Clear();
        operationQueue.Clear();

        foreach (var line in wiresString.Split(InputReader.NewLine))
        {
            if (line.Split(": ") is [var wireKey, var wireValue])
                circuit.Add(Wire.Parse(wireKey), int.Parse(wireValue));
        }

        foreach (var line in opsString.Split(InputReader.NewLine))
            operationQueue.Enqueue(Operation.Parse(line));

        while (operationQueue.TryDequeue(out var operation))
        {
            // If the operation can't be executed (because one of the wires is still not signaling)
            // move the operation back to the end of the queue.
            if (!operation.TryExecuteOn(circuit))
                operationQueue.Enqueue(operation);
        }

        var binary = new StringBuilder();

        foreach (var wire in circuit.Keys.OrderDescending())
        {
            if (wire.IsTarget)
                binary.Append(circuit[wire]);
        }

        long @decimal = long.Parse(binary.ToString(), NumberStyles.BinaryNumber);
        return @decimal;
    }

    public readonly record struct Wire(int Key, bool IsTarget) : IComparable<Wire>
    {
        public static Wire Parse(ReadOnlySpan<char> wireString)
        {
            // If it's a target wire (zxxx), extract the numeric value and use it as the key.
            // Otherwise use the hash code as key.
            bool isTarget = wireString.StartsWith('z');
            int key = isTarget ? int.Parse(wireString[1..]) : string.GetHashCode(wireString);
            return new(key, isTarget);
        }

        public int CompareTo(Wire other) => Key - other.Key;
    }

    public readonly record struct Operation(Wire WireA, Wire WireB, OperationOperand Operand, Wire WireC)
    {
        public static Operation Parse(ReadOnlySpan<char> operationString)
        {
            var wireA = Wire.Parse(operationString[..3]);
            var wireB = Wire.Parse(operationString[7..11].Trim());
            var wireC = Wire.Parse(operationString[^3..]);
            var operand = Enum.Parse<OperationOperand>(operationString[4..7].TrimEnd(), true);

            return new Operation(wireA, wireB, operand, wireC);
        }

        public bool TryExecuteOn(Circuit circuit)
        {
            if (!circuit.TryGetValue(WireA, out int wireAVal) || !circuit.TryGetValue(WireB, out int wireBVal))
                return false;

            circuit[WireC] = Operand switch
            {
                OperationOperand.AND => wireAVal & wireBVal,
                OperationOperand.OR => wireAVal | wireBVal,
                OperationOperand.XOR => wireAVal ^ wireBVal,
                _ => throw new NotImplementedException()
            };

            return true;
        }
    }

    public enum OperationOperand
    {
        AND,
        OR,
        XOR
    }
}
