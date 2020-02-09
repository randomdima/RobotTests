using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Anki.Vector;
using Google.Type;
using Color = Anki.Vector.Types.Color;

namespace RobotTests
{
    class Program
    {
        static async Task Main()
        {
            await using var robot = await RobotScheduler.Connect();
            robot.AddBehavior(ParrotModeScreen);
            robot.AddBehavior(ParrotModeWheels);
            Console.ReadKey();
        }

        static async Task ParrotModeWheels(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await robot.Motors.SetWheelMotors(50, -50);
                await robot.Motors.MoveLift(MoveDirection.Up, MotorSpeed.Medium);
                await Task.Delay(500, token);
                await robot.Motors.SetWheelMotors(-50, 50);
                await robot.Motors.MoveLift(MoveDirection.Down, MotorSpeed.Medium);
                await Task.Delay(500, token);
            }
        }

        static async Task ParrotModeScreen(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.Yellow, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.Green, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.Blue, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.White, 0, false);
                await Task.Delay(100, token);
            }
        }
    }
}