using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //tombol untuk menggerakan ke atas
    public KeyCode upButton = KeyCode.W;

    //tombol untuk menggerakan ke bawah
    public KeyCode downButton = KeyCode.S;

    //kecepatan gerak
    public float speed = 10.0f;

    //batas atas dan bawah game scene(batas bawah menggunakan minus(-))
    public float yBoundary = 9.0f;

    //Rigidbody 2D raket ini
    private Rigidbody2D rigidbody2D;

    //skor pemain
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //dapatkan kecepatan raket seimbang
        Vector2 velocity = rigidbody2D.velocity;

        //jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y(ke atas)
        if(Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        //jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y(ke bawah)
        else if(Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        //jika pemain tidak menekan tombol apa - apa, kecepatan 0
        else
        {
            velocity.y = 0.0f;
        }

        //masukan kembali kecepatannya ke Rigidbody 2D
        rigidbody2D.velocity = velocity;

        //dapatkan posisi raket sekarang
        Vector3 position = transform.position;

        //jika posisi raket melewati batas atas(yBoundary), kembali ke batas atas tersebut
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        //Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
        else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //Masukkan kembali posisinya ke transform.
        transform.position = position;
    }


    //Menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    //Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    //Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }

    //Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    //Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    //Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
