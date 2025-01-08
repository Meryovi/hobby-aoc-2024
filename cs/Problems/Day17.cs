namespace aoc24.Problems;

// https://adventofcode.com/2024/day/17
public sealed class Day17 : IProblem<string>
{
    public string Solve(string input) => Debug3bitProgramOptimized(input);

    private static string Debug3bitProgramOptimized(ReadOnlySpan<char> input)
    {
        Span<Range> ranges = stackalloc Range[2];
        input.Split(ranges, InputReader.NewLine + InputReader.NewLine);

        var registers = input[ranges[0]];
        var instructions = input[ranges[1]];

        var computer = Computer3Bit.ParseRegisters(registers);
        computer.ExecuteInstructions(instructions[9..]);

        return computer.GetOutput();
    }

    private record struct Computer3Bit()
    {
        public int RegisterA { get; set; }
        public int RegisterB { get; set; }
        public int RegisterC { get; set; }

        private readonly System.Text.StringBuilder output = new();

        public static Computer3Bit ParseRegisters(ReadOnlySpan<char> registerString)
        {
            Span<Range> ranges = stackalloc Range[3];
            registerString.Split(ranges, InputReader.NewLine);

            var computer = new Computer3Bit
            {
                RegisterA = int.Parse(registerString[ranges[0]][11..]),
                RegisterB = int.Parse(registerString[ranges[1]][11..]),
                RegisterC = int.Parse(registerString[ranges[2]][11..])
            };
            return computer;
        }

        public void ExecuteInstructions(ReadOnlySpan<char> operationString)
        {
            Span<Instruction> instructions = stackalloc Instruction[(int)Math.Ceiling((double)operationString.Length / 4)];
            for (int pointer = 0; pointer < operationString.Length; pointer += 4)
            {
                var opCode = (OpCode)(operationString[pointer] - '0');
                int operation = operationString[pointer + 2] - '0';
                instructions[pointer / 4] = new Instruction(opCode, operation);
            }
            ExecuteInstructions(instructions);
        }

        public void ExecuteInstructions(ReadOnlySpan<Instruction> instructions)
        {
            int pointer = 0;
            while (pointer < instructions.Length)
            {
                var (opCode, operation) = instructions[pointer];
                pointer = ApplyOperation(opCode, operation, pointer);
            }
        }

        public int ApplyOperation(OpCode opcode, int operand, int pointer)
        {
            switch (opcode)
            {
                case OpCode.Adv:
                    RegisterA /= (int)Math.Pow(2, GetComboValue(operand));
                    break;
                case OpCode.Bxl:
                    RegisterB ^= operand;
                    break;
                case OpCode.Bst:
                    RegisterB = GetComboValue(operand) % 8;
                    break;
                case OpCode.Jnz:
                    if (RegisterA != 0)
                        return operand / 2;
                    break;
                case OpCode.Bxc:
                    RegisterB ^= RegisterC;
                    break;
                case OpCode.Out:
                    output.Append(GetComboValue(operand) % 8).Append(',');
                    break;
                case OpCode.Bdv:
                    RegisterB = RegisterA / (int)Math.Pow(2, GetComboValue(operand));
                    break;
                case OpCode.Cdv:
                    RegisterC = RegisterA / (int)Math.Pow(2, GetComboValue(operand));
                    break;
            }
            return pointer + 1;
        }

        public readonly int GetComboValue(int operand) =>
            operand switch
            {
                <= 3 => operand,
                4 => RegisterA,
                5 => RegisterB,
                6 => RegisterC,
                _ => operand
            };

        public readonly string GetOutput() => output.Remove(output.Length - 1, 1).ToString();
    }

    public readonly record struct Instruction(OpCode OpCode, int Operand)
    {
        public int OpCodeInt => (int)OpCode;
    }

    public enum OpCode : int
    {
        Adv = 0,
        Bxl = 1,
        Bst = 2,
        Jnz = 3,
        Bxc = 4,
        Out = 5,
        Bdv = 6,
        Cdv = 7
    }
}
