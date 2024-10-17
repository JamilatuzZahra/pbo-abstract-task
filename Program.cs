using System;

public interface IKemampuan
{
    void GunakanKemampuan(Robot target);
    bool CooldownSelesai();
    void ResetCooldown();
}

public abstract class Robot
{
    public string nama;
    public int energi;
    public int armor;
    public int serangan;

    public Robot(string nama,int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public abstract void GunakanKemampuan(IKemampuan kemampuan);
    public abstract void Serang(Robot target);

    public void CetakInformasi()
    {
        Console.WriteLine($"Nama: {nama}");
        Console.WriteLine($"Energi: {energi}");
        Console.WriteLine($"Armor: {armor}");
        Console.WriteLine($"Serangan: {serangan}");
    }

    public void PulihkanEnergi(int jumlah)
    {
        energi += jumlah;
        Console.WriteLine($"{nama} memulihkan energi dan energi nya sekarang adalah: {energi}");
    }
}

public class RobotBiasa : Robot
{
    public RobotBiasa(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }
    public override void Serang(Robot target)
    {
        int damage = Math.Max(0, this.serangan - target.armor);
        target.energi -= damage;
        Console.WriteLine($"{nama} sedang menyerang {target.nama} dengan serangan {damage}");
    }

    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.GunakanKemampuan(this);
    }

}

public class BosRobot : Robot
{
    private int pertahanan;
    public BosRobot(string nama, int energi, int armor, int serangan, int pertahanan)
        : base(nama, energi, armor, serangan)
    {
        this.pertahanan = pertahanan;
    }

    public override void Serang(Robot target)
    {
        int damage = Math.Max(0, this.serangan - target.armor + pertahanan);
        target.energi -= damage;
        Console.WriteLine($"{nama} (Bos) menyerang {target.nama} dengan serangan {damage}");
    }
    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.GunakanKemampuan(this);
    }
    public void Mati()
    {
        Console.WriteLine($"{nama} telah mati yeayy");
    }
}

public class Repair : IKemampuan
{
    private int cooldown;
    private int waktuCooldown;

    public Repair()
    {
        this.cooldown = 3;
        this.waktuCooldown = 0;
    }

    public void GunakanKemampuan(Robot target)
    {
        if (CooldownSelesai())
        {
            target.PulihkanEnergi(15);
            Console.WriteLine($"{target.nama} sedang  menggunakan kemampuan Repair, sehingga memulihkan 15 energi");
            ResetCooldown();
        }
        else
        {
            Console.WriteLine($"{target.nama} tida bisa menggunakan repair, cooldown belum selesai");
        }
    }

    public bool CooldownSelesai()
    {
        return waktuCooldown == 0;
    }
    public void ResetCooldown()
    {
        waktuCooldown = cooldown;
    }

    public void KurangiCooldown()
    {
        if (waktuCooldown > 0)
        {
            waktuCooldown--;
        }
    }
}

public class ElectricShock : IKemampuan
{
    private int cooldown;
    private int waktuCooldownn;

    public ElectricShock()
    {
        this.cooldown = 2;
        this.waktuCooldownn = 0;
    }
    public void GunakanKemampuan(Robot target)
    {
        if (CooldownSelesai())
        {
            target.energi -= 5;
            Console.WriteLine($"{target.nama} sedang diserang dengan electric shock, kehilangan 5 energi");
            ResetCooldown();

        }
        else
        {
            Console.WriteLine($"{target.nama} tidak bisa menggunakan electrick shock, cooldown belum selesai");
        }
    }
    public bool CooldownSelesai()
    {
        return waktuCooldownn == 0;
    }

    public void ResetCooldown()
    {
        waktuCooldownn = cooldown;
    }
    public void KurangiCooldown()
    {
        if (waktuCooldownn > 0)
        {
            waktuCooldownn--;
        }
    }
}
public class PlasmaCannon : IKemampuan
{
    private int cooldown;
    private int waktuCooldownn;

    public PlasmaCannon()
    {
        this.cooldown = 4;
        this.waktuCooldownn = 0;
    }

    public void GunakanKemampuan(Robot target)
    {
        if (CooldownSelesai())
        {
            int damage = 15;
            target.energi -= damage;
            Console.WriteLine($"{target.nama} sedang diserang menggunakan plasma cannon, kehilangan {damage} energi");
            ResetCooldown();
        }
        else
        {
            Console.WriteLine($"{target.nama} tidak bisa menggunakan plasma cannon, karena cooldown belum selesai");
        }
    }
    public bool CooldownSelesai()
    {
        return waktuCooldownn == 0;
    }
    public void ResetCooldown()
    {
        waktuCooldownn = cooldown;
    }
    public void KurangiCooldown()
    {
        if (waktuCooldownn > 0)
        {
            waktuCooldownn--;
        }
    }
}

public  class SuperShield : IKemampuan
{
    private int cooldown;
    private int waktuCooldown;

    public SuperShield()
    {
        this.cooldown = 5;
        this.waktuCooldown = 0;
    }

    public void GunakanKemampuan(Robot target)
    {
        if (CooldownSelesai())
        {
            target.CetakInformasi();
            Console.WriteLine($"{target.nama} sedang menggunakan super shield, armor meningkat sementara");
            ResetCooldown();
        }
        else
        {
            Console.WriteLine($"{target.nama} tidak bisa menggunakan super shield, cooldown belum selesai");
        }
    }
    public bool CooldownSelesai()
    {
        return waktuCooldown == 0;
    }
    public void ResetCooldown()
    {
        waktuCooldown = cooldown;
    }
    public void KurangiCooldown()
    {
        if (waktuCooldown > 0)
        {
            waktuCooldown--;
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Robot robot1 = new RobotBiasa("Robot Biru", 110, 10, 20);
        Robot robot2 = new BosRobot("Bos Robot Pink", 150, 15, 30, 5);
        IKemampuan repair = new Repair();
        IKemampuan electricShock = new ElectricShock();
        IKemampuan plasmaCannon = new PlasmaCannon();
        IKemampuan superShield = new SuperShield();

        robot1.CetakInformasi();
        Console.WriteLine();
        robot2.CetakInformasi();
        Console.WriteLine();

        robot1.Serang(robot2);
        robot2.Serang(robot1);
        Console.WriteLine();

        robot1.GunakanKemampuan(repair);
        robot2.GunakanKemampuan(electricShock);
        Console.WriteLine();

        ((Repair)repair).KurangiCooldown();
        ((ElectricShock)electricShock).KurangiCooldown();
        Console.WriteLine();

        robot1.PulihkanEnergi(10);
        robot2.PulihkanEnergi(10);
        Console.WriteLine();

        robot1.GunakanKemampuan(plasmaCannon);
        robot2.GunakanKemampuan(superShield);
        Console.WriteLine();

        robot1.CetakInformasi();
        robot2.CetakInformasi();
        Console.WriteLine();
    }
}