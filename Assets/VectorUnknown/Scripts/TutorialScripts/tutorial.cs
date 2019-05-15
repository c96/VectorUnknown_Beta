using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class tutorial : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playable_director;
    [SerializeField]
    private List< TimelineAsset> timelines = new List< TimelineAsset>();
    [SerializeField]
    private List<TimelineAsset> reverse_timelines = new List<TimelineAsset>();
    public TextMeshProUGUI instruction, title;
    private string[] tutorial_messages = new string[7]
    {
        "\tThe minimap in the top right corner shows your position on the grid from a top down perspective.",
        "\tThese are the choices you have available to you. This panel displays the vectors you have available to you for solving the current puzzle.",
        "\tThis is the player info panel. This shows your current position, the goal's position, and the number of attempts you have left.",
        "\tThe log panel will log your progress through the level. Every time you move the bunny by entering a formula, the formula is logged to this panel. It contains a history of each step you take.",
        "\tThis is the formula bar. This is where you will drag the choice vectors and manipulate them in order to move the bunny to your intended location on the grid.",
        "\tThe timer keeps track of how long you've been playing the current level. You can click on the timer to make it disapear and reappear.",
        "\tThese are the options buttons. Click then hamburger icon to expand the menu. In game you will be able to restart the level or return to level select from here.",
    };
    private string[] tutorial_titles = new string[7]
    {
        "1. Minimap",
        "2. Choice Vectors",
        "3. Player Info",
        "4. Log",
        "5. Formula Bar",
        "6. Timer",
        "7. Options",
    };

    private int current_message = -1;

    private void set_msg()
    {
        instruction.text = tutorial_messages[current_message];
        title.text = tutorial_titles[current_message];
    }

    public void next_message()
    {
        if (current_message >= 6)
            return;

        current_message++;
        set_msg();

        playable_director.Play(timelines[current_message]);
    }

    public void prev_message()
    {
        if (current_message <= 0)
            return;

        current_message--;
        set_msg();

        playable_director.Play(reverse_timelines[current_message]);
    }
}
