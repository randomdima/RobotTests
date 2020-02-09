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
            robot.AddBehavior(ParrotModeLift);
            robot.AddBehavior(ParrotModeHead);
            Console.ReadKey();
        }
        static async Task ParrotModeLift(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await robot.Motors.MoveLift(MoveDirection.Up, MotorSpeed.Slow);
                await Task.Delay(500, token);
                await robot.Motors.MoveLift(MoveDirection.Down, MotorSpeed.Fast);
                await robot.Motors.MoveLift(MoveDirection.Up, MotorSpeed.Fast);
                await robot.Motors.MoveLift(MoveDirection.Down, MotorSpeed.Fast);
            }
        }
        static async Task ParrotModeHead(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await robot.Motors.MoveHead(MoveDirection.Up, MotorSpeed.Slow);
                await Task.Delay(500, token);
                await robot.Motors.MoveHead(MoveDirection.Down, MotorSpeed.Fast);

            }
        }
        static async Task ParrotModeWheels(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000, token);
                await robot.Motors.SetWheelMotors(100, -100);
            }
        }

        static async Task ParrotModeScreen(Robot robot, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await robot.Screen.DisplaySolidColor(Color.Black, 0, false);
                await Task.Delay(500, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Black, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Black, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(100, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Black, 0, false);
                await Task.Delay(200, token);
                await robot.Screen.DisplaySolidColor(Color.Red, 0, false);
                await Task.Delay(100, token);
            }
        }
    }
}