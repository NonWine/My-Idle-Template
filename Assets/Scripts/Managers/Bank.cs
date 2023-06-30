using System.Collections;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance;

    [SerializeField] private TextMeshProUGUI _coinsText;

    private int _coinsCount;

    public int CoinsCount => _coinsCount;

    private void Awake()
    {
        Instance = this;
        _coinsCount = PlayerPrefs.GetInt("CoinsCount");
    }

    private void Start()
    {
        _coinsText.SetText(CoinCountToString(_coinsCount));
    }

    public void AddCoins(int count) => StartCoroutine(AddCoinsCor(count));

    public void SubtractCoins(int count) => StartCoroutine(SubtractCoinsCor(count));

    public void ReduceCoins(int mod)
    {    
        if (mod == 0)
        {
            mod = 1;
            _coinsCount -= mod;
            _coinsText.SetText(CoinCountToString(_coinsCount));
        }
        else
        {
            _coinsCount -= mod;
            _coinsText.SetText(CoinCountToString(_coinsCount));
        }
    }


    private IEnumerator AddCoinsCor(int count)
    {
        float counter = _coinsCount;
        _coinsCount += count;
        PlayerPrefs.SetInt("CoinsCount", _coinsCount);
        while (counter < _coinsCount) 
        {
            counter += count / 5f;

            _coinsText.SetText(CoinCountToString(Mathf.RoundToInt(counter)));

            yield return null;
        }
        _coinsText.SetText(CoinCountToString(_coinsCount));
    }

    private IEnumerator SubtractCoinsCor(int count)
    {
        float counter = _coinsCount;
        _coinsCount -= count;
        PlayerPrefs.SetInt("CoinsCount", _coinsCount);
        while (counter > _coinsCount)
        {
            counter -= count / 10f;

            _coinsText.SetText(CoinCountToString(Mathf.RoundToInt(counter)));

            yield return null;
        }
        _coinsText.SetText(CoinCountToString(_coinsCount));
    }

    public static string CoinCountToString(int count)
    {
        string text = null;
        if (count > 1000000000) text = (count / 1000000000f).ToString("F") + "B";
        else if (count > 1000000) text = (count / 1000000f).ToString("F") + "M";
        else if (count > 1000) text = (count / 1000f).ToString("F") + "K";
        else { text = count.ToString(); }
        return text;
    }
}